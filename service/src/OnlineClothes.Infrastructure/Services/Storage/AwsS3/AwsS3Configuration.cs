using OnlineClothes.Infrastructure.StandaloneConfigurations;

namespace OnlineClothes.Infrastructure.Services.Storage.AwsS3;

public class AwsS3Configuration
{
	public AwsCredential Credential { get; init; } = null!;
	public string BucketName { get; init; } = null!;
	public string Region { get; init; } = null!;
	public string Endpoint { get; init; } = null!;
}
