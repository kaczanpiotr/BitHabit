using Habits.Domain.Entities;
using System.Linq.Expressions;

namespace Habits.Domain.Repositories;

public interface IHabitsRepository
{
    Task<Habit> GetAsync(HabitId id);
    Task<IReadOnlyList<Habit>> BrowseAsync();
    Task<IReadOnlyList<Habit>> BrowseAsync(Expression<Func<Habit, bool>> predicate);
    Task AddAsync(Habit habit);
    Task Update(Habit habit);
    Task Remove(Habit habit);
}
