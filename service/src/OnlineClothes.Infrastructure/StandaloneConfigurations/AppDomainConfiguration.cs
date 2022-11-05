using System.Text;

namespace OnlineClothes.Infrastructure.StandaloneConfigurations;

public class AppDomainConfiguration
{
	public string Host { get; init; } = null!;
	public ushort Port { get; init; }
	public string Protocol { get; init; } = null!;

	public override string ToString()
	{
		var sb = new StringBuilder($"{Protocol}://{Host}");
		if (Port != 0)
		{
			sb.Append($":{Port}");
		}

		return sb.ToString();
	}
}
