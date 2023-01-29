using System.ComponentModel.DataAnnotations.Schema;
using MediatR;
using Newtonsoft.Json;

namespace OnlineClothes.BuildIn.Entity.Event;

public abstract class SupportDomainEvent : ISupportDomainEvent
{
	private readonly List<DomainEventPayload> _eventPayloads = new();

	[JsonIgnore]
	[NotMapped]
	public IReadOnlyCollection<DomainEventPayload> EventPayloads => _eventPayloads.AsReadOnly();

	public void AddEventPayload(string? key = null, object? value = null)
	{
		var payloadValue = value ?? this;
		var eventPayload = new DomainEventPayload(ResolvePayloadKey(key), payloadValue);

		_eventPayloads.Add(eventPayload);
	}

	private string ResolvePayloadKey(string? key = null)
	{
		return key ?? GetType().Name;
	}
}

public interface ISupportDomainEvent : INotification
{
	/// <summary>
	/// Payload of domain event in KEY - VALUE format.
	/// </summary>
	IReadOnlyCollection<DomainEventPayload> EventPayloads { get; }

	/// <summary>
	/// Add payload to domain events, if key is null typeof object wil be used as payload key, if value is null entity will add
	/// itself as payload value.
	/// to payload
	/// </summary>
	/// <param name="key"></param>
	/// <param name="value"></param>
	void AddEventPayload(string? key = null, object? value = null);
}

public record DomainEventPayload(string Key, object? Value);
