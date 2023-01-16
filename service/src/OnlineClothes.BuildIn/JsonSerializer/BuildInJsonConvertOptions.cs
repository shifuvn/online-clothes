using Newtonsoft.Json;

namespace OnlineClothes.BuildIn.JsonSerializer;

public static class BuildInJsonConvertOptions
{
	public static JsonSerializerSettings Default => BuildDefault();

	public static JsonSerializerSettings BuildDefault()
	{
		return new JsonSerializerSettings
		{
			ReferenceLoopHandling = ReferenceLoopHandling.Ignore
		};
	}
}
