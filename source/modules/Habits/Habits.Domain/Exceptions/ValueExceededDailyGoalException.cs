using Shared.Abstractions.Exceptions;

namespace Habits.Domain.Exceptions;
public class ValueExceededDailyGoalException : BitHabitException
{
    public int Value { get; set; }

    public ValueExceededDailyGoalException(int value)
        : base($"Value {value} exceeded daily goal exception")
    {
        Value = value;
    }
}
