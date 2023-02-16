using Shared.Abstractions.Exceptions;

namespace Habits.Domain.Exceptions;
public class ReminderAlreadyExistsException : BitHabitException
{
    public TimeSpan TimeSpan { get; }

    public ReminderAlreadyExistsException(TimeSpan timeSpan)
        : base($"Reminder {timeSpan} already exists")
    {
        TimeSpan = timeSpan;
    }
}
