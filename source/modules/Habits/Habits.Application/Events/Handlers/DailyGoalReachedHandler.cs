using Habits.Domain.Events;
using Habits.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Shared.Abstractions.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Habits.Application.Events.Handlers;
internal class DailyGoalReachedHandler : IEventHandler<DailyGoalReached>
{
    private readonly IHabitsRepository _habitRepository;
    private readonly ILogger<DailyGoalReachedHandler> _logger;

    public DailyGoalReachedHandler(IHabitsRepository habitRepository, ILogger<DailyGoalReachedHandler> logger)
    {
        _habitRepository = habitRepository;
        _logger = logger;
    }

    public async Task HandleAsync(DailyGoalReached @event, CancellationToken cancellationToken = default)
    {
        var habit = await _habitRepository.GetAsync(@event.HabitId);

        if(habit is not null)
            _logger.LogInformation($"Congrats! You have been reached {habit.Name} daily goal in {@event.Date.ToShortDateString()}");
    }
}
