namespace OnlineClothes.Domain.Common;

public class Fullname
{
	public Fullname(string firstName, string lastName)
	{
		if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
		{
			throw new InvalidOperationException("FirstName or LastName is invalid");
		}

		FirstName = firstName;
		LastName = lastName;
	}

	public string FirstName { get; set; }
	public string LastName { get; set; }

	public string FullName => BuildFullName();

	public static Fullname Create(string firstName, string lastName)
	{
		return new Fullname(firstName, lastName);
	}

	public static Fullname Create(string fullName)
	{
		var fullNameSpan = fullName.AsSpan();
		var firstDelimiter = fullNameSpan.IndexOf(' ');

		if (firstDelimiter == -1)
		{
			throw new InvalidOperationException("FirstName or LastName is invalid");
		}

		var nextDelimiter = firstDelimiter + 1;

		return new Fullname(
			fullNameSpan[..firstDelimiter].ToString(),
			fullNameSpan.Slice(nextDelimiter, fullNameSpan.Length - nextDelimiter).ToString());
	}

	private string BuildFullName()
	{
		return FirstName + " " + LastName;
	}
}
