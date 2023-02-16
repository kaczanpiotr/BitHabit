using Habits.Domain.Entities;
using Habits.Domain.Repositories;
using Shared.Abstractions.Requests;
using System.Threading;
using System.Threading.Tasks;

namespace Habits.Application.Commands.Handlers;

internal class RemoveHabitHandler : IRequestHandler<RemoveHabit>
{
    private readonly IHabitsRepository _habitsRepository;

    public RemoveHabitHandler(IHabitsRepository habitsRepository)
    {
        _habitsRepository = habitsRepository;
    }

    public async Task<Task> Handle(RemoveHabit request, CancellationToken cancellationToken)
    {
        var habit = await _habitsRepository.GetAsync(new HabitId(request.Id));
        await _habitsRepository.Remove(habit);
        return Task.CompletedTask;
    }
}
