using Newtonsoft.Json;
using Shared.Abstractions.Domain;

namespace Shared.Infrastructure.Outbox;

internal static class Extensions
{
    public static OutboxMessage ToOutboxMessage(this IDomainEvent domainEvent)
    {
        return new OutboxMessage()
        {
            Id = Guid.NewGuid(),
            Type = domainEvent.GetType().Name,
            Content = JsonConvert.SerializeObject(domainEvent, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All }),
            OccurredOn = DateTime.UtcNow
        };
    }
}
