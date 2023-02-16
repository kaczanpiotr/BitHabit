using Habits.Domain.Entities;
using Habits.Domain.Repositories;
using Shared.Abstractions.Requests;
using System.Threading;
using System.Threading.Tasks;

namespace Habits.Application.Commands.Handlers;
public class SetOrCreateProgressHandler : IRequestHandler<SetOrCreateProgress>
{
    private readonly IHabitsRepository _habitsRepository;

    public SetOrCreateProgressHandler(IHabitsRepository habitsRepository)
    {
        _habitsRepository = habitsRepository;
    }

    public async Task<Task> Handle(SetOrCreateProgress request, CancellationToken cancellationToken = default)
    {
        var habitId = new HabitId(request.HabitId);
        var habit = await _habitsRepository.GetAsync(habitId);

        habit.SetOrCreateProgress(request.Date, request.Value);
        await _habitsRepository.Update(habit);

        return Task.CompletedTask;
    }
}
