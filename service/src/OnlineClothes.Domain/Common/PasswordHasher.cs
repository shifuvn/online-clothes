using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace OnlineClothes.Domain.Common;

/// <summary>
/// Implement the standard Identity password hashing
/// </summary>
public static class PasswordHasher
{
	private const int SaltSize = 128 / 8;
	private const int NumBytesByRequested = 256 / 8;
	private static readonly PasswordHasherOptions DefaultOptions = new();

	public static string Hash(string password)
	{
		return Convert.ToBase64String(HashV3(password, DefaultOptions.DefaultRng,
			KeyDerivationPrf.HMACSHA512,
			DefaultOptions.IterCount,
			SaltSize,
			NumBytesByRequested));
	}

	public static PasswordVerificationResult Verify(string hashedPassword, string rawProvidedPassword)
	{
		ArgumentNullException.ThrowIfNull(hashedPassword);
		ArgumentNullException.ThrowIfNull(rawProvidedPassword);

		var decodedHashed = Convert.FromBase64String(hashedPassword);

		if (decodedHashed.Length == 0)
		{
			return PasswordVerificationResult.Failed;
		}

		switch (decodedHashed[0])
		{
			case 0x00:
			case 0x01:
				return VerifyHashV3(decodedHashed, rawProvidedPassword)
					? PasswordVerificationResult.Success
					: PasswordVerificationResult.Failed;

			default:
				return PasswordVerificationResult.Failed;
		}
	}

	private static byte[] HashV3(string password, RandomNumberGenerator rng,
		KeyDerivationPrf prf,
		int iterCount,
		int saltSize,
		int numBytesRequested)
	{
		var salt = new byte[saltSize];
		rng.GetBytes(salt);
		var subKey = KeyDerivation.Pbkdf2(password, salt, prf, iterCount, numBytesRequested);

		var outputBytes = new byte[13 + salt.Length + subKey.Length];
		outputBytes[0] = 0x01; // format marker
		WriteNetworkByteOrder(outputBytes, 1, (uint)prf);
		WriteNetworkByteOrder(outputBytes, 5, (uint)iterCount);
		WriteNetworkByteOrder(outputBytes, 9, (uint)saltSize);

		Buffer.BlockCopy(salt, 0, outputBytes, 13, salt.Length);
		Buffer.BlockCopy(subKey, 0, outputBytes, 13 + saltSize, subKey.Length);

		return outputBytes;
	}

	private static bool VerifyHashV3(byte[] hashPassword, string password)
	{
		try
		{
			// read header info
			var prf = (KeyDerivationPrf)ReadNetworkByteOrder(hashPassword, 1);
			var iterCount = (int)ReadNetworkByteOrder(hashPassword, 5);
			var saltLength = (int)ReadNetworkByteOrder(hashPassword, 9);

			if (saltLength < 128 / 8)
			{
				return false;
			}

			var salt = new byte[saltLength];
			Buffer.BlockCopy(hashPassword, 13, salt, 0, saltLength);

			var subKeyLength = hashPassword.Length - 13 - salt.Length;
			if (subKeyLength < 128 / 8)
			{
				return false;
			}

			var expectedSubKey = new byte[subKeyLength];
			Buffer.BlockCopy(hashPassword, 13 + saltLength, expectedSubKey, 0, expectedSubKey.Length);

			// incoming hash and verify
			var actualKey = KeyDerivation.Pbkdf2(password, salt, prf, iterCount, subKeyLength);

			return CryptographicOperations.FixedTimeEquals(actualKey, expectedSubKey);
		}
		catch (Exception)
		{
			return false;
		}
	}

	public static string RandomPassword(int length)
	{
		var tokenBuffer = new byte[length];
		DefaultOptions.DefaultRng.GetBytes(tokenBuffer);

		return Convert.ToBase64String(tokenBuffer);
	}

	private static uint ReadNetworkByteOrder(byte[] buffer, int offset)
	{
		return ((uint)buffer[offset + 0] << 24)
		       | ((uint)buffer[offset + 1] << 16)
		       | ((uint)buffer[offset + 2] << 8)
		       | buffer[offset + 3];
	}


	private static void WriteNetworkByteOrder(byte[] buffer, int offset, uint value)
	{
		buffer[offset + 0] = (byte)(value >> 24);
		buffer[offset + 1] = (byte)(value >> 16);
		buffer[offset + 2] = (byte)(value >> 8);
		buffer[offset + 3] = (byte)(value >> 0);
	}
}

public class PasswordHasherOptions
{
	public readonly RandomNumberGenerator DefaultRng = RandomNumberGenerator.Create();

	public int IterCount { get; set; } = 100_000;
}

public enum PasswordVerificationResult
{
	Failed = 0,
	Success
}
