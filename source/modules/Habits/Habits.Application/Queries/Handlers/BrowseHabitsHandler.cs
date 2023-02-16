using Habits.Application.DTO;
using Habits.Domain.Repositories;
using Shared.Abstractions.Requests;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Habits.Application.Queries.Handlers;

internal class BrowseHabitsHandler : IRequestHandler<BrowseHabits, IReadOnlyList<HabitDto>>
{
    private readonly IHabitsRepository _habitsRepository;

    public BrowseHabitsHandler(IHabitsRepository habitsRepository)
    {
        _habitsRepository = habitsRepository;
    }

    public async Task<IReadOnlyList<HabitDto>> Handle(BrowseHabits request, CancellationToken cancellationToken)
    {
        var habits = await _habitsRepository.BrowseAsync();
        return habits.Select(h => h.AsDto()).ToArray();
    }
}
