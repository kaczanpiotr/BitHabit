using Habits.Domain.Entities;
using Habits.Domain.Repositories;
using Shared.Abstractions.Requests;
using System.Threading;
using System.Threading.Tasks;

namespace Habits.Application.Commands.Handlers;

public class AddReminderHandler : IRequestHandler<AddReminder>
{
    private readonly IHabitsRepository _habitsRepository;

    public AddReminderHandler(IHabitsRepository habitsRepository)
    {
        _habitsRepository = habitsRepository;
    }

    public async Task<Task> Handle(AddReminder request, CancellationToken cancellationToken = default)
    {
        var habitId = new HabitId(request.HabitId);
        var habit = await _habitsRepository.GetAsync(habitId);
        var newReminder = Reminder.Create(request.Time);

        habit.AddReminder(newReminder);
        await _habitsRepository.Update(habit);

        return Task.CompletedTask;
    }
}

