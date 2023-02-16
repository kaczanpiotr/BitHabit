using Shared.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Shared.Abstractions.BuildingBlocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Infrastructure.Database.Interceptors;

internal sealed class ConvertDomainEventsToOutboxMessagesInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, 
        InterceptionResult<int> result, 
        CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;

        if(context == null)
            return base.SavingChangesAsync(eventData, result, cancellationToken);

        var x = context.ChangeTracker.Entries<AggregateRoot>();

        var outboxMessages = x.Select(ar => ar.Entity).SelectMany(aggregate =>
        {
            var domainEvents = aggregate.GetDomainEvents();
            aggregate.ClearDomainEvents();
            return domainEvents;
        }).Select(e => e.ToOutboxMessage());

        context.Set<OutboxMessage>().AddRange(outboxMessages);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
