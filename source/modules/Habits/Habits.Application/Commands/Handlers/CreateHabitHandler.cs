using Habits.Domain.Entities;
using Habits.Domain.Enums;
using Habits.Domain.Repositories;
using Shared.Abstractions.Requests;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Habits.Application.Commands.Handlers;

internal class CreateHabitHandler : IRequestHandler<CreateHabit>
{
    private readonly IHabitsRepository _habitsRepository;

    public CreateHabitHandler(IHabitsRepository habitsRepository)
    {
        _habitsRepository = habitsRepository;
    }

    public async Task<Task> Handle(CreateHabit request, CancellationToken cancellationToken = default)
    {
        var habit = Habit.Create(
            request.Name,
            (DaysOfWeek)request.DaysOfWeek,
            request.DailyGoal,
            request.Reminders.Select(r => Reminder.Create(r)));

        await _habitsRepository.AddAsync(habit);

        return Task.CompletedTask;
    }
}
