using Habits.Domain.Entities;
using Habits.Domain.Repositories;
using Shared.Abstractions.Requests;
using System.Threading;
using System.Threading.Tasks;

namespace Habits.Application.Commands.Handlers;

internal class RemoveReminderHandler : IRequestHandler<RemoveReminder>
{
    private readonly IHabitsRepository _habitsRepository;

    public RemoveReminderHandler(IHabitsRepository habitsRepository)
    {
        _habitsRepository = habitsRepository;
    }

    public async Task<Task> Handle(RemoveReminder request, CancellationToken cancellationToken = default)
    {
        var habitId = new HabitId(request.HabitId);
        var habit = await _habitsRepository.GetAsync(habitId);

        habit.RemoveReminder(request.Time);
        await _habitsRepository.Update(habit);

        return Task.CompletedTask;
    }
}
