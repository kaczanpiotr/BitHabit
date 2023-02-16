using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;
using Shared.Abstractions.Dispatchers;
using Shared.Infrastructure.Database;

namespace Shared.Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob : IJob
{
    private readonly BitHabitContext _bitHabitContext;
    private readonly IDispatcher _dispatcher;

    public ProcessOutboxMessagesJob(BitHabitContext bitHabitContext, IDispatcher dispatcher)
    {
        _bitHabitContext = bitHabitContext;
        _dispatcher = dispatcher;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await _bitHabitContext.OutboxMessages.Where(m => m.ProcessedOn == null).ToListAsync();

        foreach (var message in messages)
        {
            dynamic domainEvent = JsonConvert.DeserializeObject(message.Content, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });

            if (domainEvent is null)
                continue;

            await _dispatcher.Notify(domainEvent);

            message.ProcessedOn = DateTime.UtcNow;
        }

        await _bitHabitContext.SaveChangesAsync(context.CancellationToken);
    }
}
