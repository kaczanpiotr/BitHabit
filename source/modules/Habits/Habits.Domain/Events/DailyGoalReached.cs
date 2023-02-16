using Shared.Abstractions.Domain;

namespace Habits.Domain.Events;
public record DailyGoalReached(Guid HabitId, DateTime Date) : IDomainEvent;