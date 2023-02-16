using Shared.Abstractions.Exceptions;

namespace Habits.Domain.Exceptions;
public class ReminderNotFoundException : BitHabitException
{
    public TimeSpan TimeSpan { get; }

    public ReminderNotFoundException(TimeSpan timeSpan)
        : base($"Reminder {timeSpan} not found")
    {
        TimeSpan = timeSpan;
    }
}
