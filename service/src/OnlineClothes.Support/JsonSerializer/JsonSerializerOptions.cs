using System.Text.Json.Serialization;

namespace OnlineClothes.Support.JsonSerializer;

public static class JsonSerializerOptions
{
	public static readonly System.Text.Json.JsonSerializerOptions Default;

	static JsonSerializerOptions()
	{
		Default = BuildDefaultOptions();
	}

	public static System.Text.Json.JsonSerializerOptions BuildDefaultOptions(
		bool useCamelCaseNaming = true,
		bool useJsonStringEnumConverter = true)
	{
		var result = new System.Text.Json.JsonSerializerOptions
		{
			DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
		};
		if (useCamelCaseNaming)
		{
			result.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
		}

		if (useJsonStringEnumConverter)
		{
			result.Converters.Add(new JsonStringEnumConverter());
		}

		return result;
	}
}
