namespace Shared.Abstractions.Exceptions;

public abstract class BitHabitException : Exception
{
    protected BitHabitException(string message) : base(message)
    {
    }
}