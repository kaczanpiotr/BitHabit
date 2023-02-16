namespace Habits.Domain.Entities;

public class Reminder
{
    public HabitId HabitId { get; private set; }
    public TimeSpan Time { get; }

    private Reminder() { }
    
    public Reminder(TimeSpan time)
    {
        Time = time;
    }
    
    public static Reminder Create(TimeSpan time)
    {
        return new Reminder(time);
    }

    public override bool Equals(object obj)
    {
        return obj is Reminder reminder &&
               Time.Equals(reminder.Time);
    }

    public override int GetHashCode()
    {
        return Time.GetHashCode();
    }
}