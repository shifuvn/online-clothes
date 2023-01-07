using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace OnlineClothes.Support.Entity.Event;

public class SupportDomainEvent : ISupportDomainEvent
{
	[JsonIgnore] [NotMapped] public List<KeyValuePair<string, object?>> PayloadEvents { get; } = new();

	public void AppendPayloadEvent(string payloadEventName, object? eventPayload = null)
	{
		PayloadEvents.Add(new KeyValuePair<string, object?>(payloadEventName, eventPayload));
	}

	public virtual KeyValuePair<string, object?> FindPayload(string keyName)
	{
		return PayloadEvents.Count == 0 ? default : PayloadEvents.FirstOrDefault(q => q.Key.Equals(keyName));
	}

	public virtual TPayload? FindPayload<TPayload>(string keyName)
	{
		var payload = FindPayload(keyName);
		return System.Text.Json.JsonSerializer.Deserialize<TPayload>(payload.Value?.ToString()!);
	}

	public bool Contains(string keyName)
	{
		return PayloadEvents.Count != 0 && PayloadEvents.Select(q => q.Key).ToHashSet().Contains(keyName);
	}
}

public interface ISupportDomainEvent
{
	List<KeyValuePair<string, object?>> PayloadEvents { get; }

	/// <summary>
	/// Add payload event content
	/// </summary>
	/// <param name="payloadEventName"></param>
	/// <param name="eventPayload"></param>
	void AppendPayloadEvent(string payloadEventName, object? eventPayload = null);

	KeyValuePair<string, object?> FindPayload(string keyName);
	TPayload? FindPayload<TPayload>(string keyName);

	bool Contains(string keyName);
}
