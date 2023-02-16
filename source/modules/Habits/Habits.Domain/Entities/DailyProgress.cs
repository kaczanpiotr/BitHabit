namespace Habits.Domain.Entities;

public class DailyProgress
{
    public HabitId HabitId { get; }
    public DateTime Date { get; }
    public short Value { get; private set; }

    private DailyProgress() { }

    private DailyProgress(HabitId habitId, DateTime date)
    {
        HabitId = habitId;
        Date = date;
    }

    public static DailyProgress Create(HabitId habitId, DateTime date)
    {
        return new DailyProgress(habitId, date);
    }

    public void SetValue(short value)
    {
        Value = value;
    }

    public override bool Equals(object obj)
    {
        return obj is DailyProgress progress &&
               Date == progress.Date;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Date);
    }
}