using Habits.Domain.Enums;
using Habits.Domain.Events;
using Habits.Domain.Exceptions;
using Shared.Abstractions.BuildingBlocks;

namespace Habits.Domain.Entities;

public class Habit : AggregateRoot
{ 
    private HashSet<DailyProgress> _progress = new();
    private HashSet<Reminder> _reminders = new();

    public HabitId Id { get; private set; }
    public string Name { get; private set; }
    public DaysOfWeek Days { get; private set; }
    public short DailyGoal { get; private set; }
    public IEnumerable<DailyProgress> Progress
    {
        get => _progress;
        set => _progress = new HashSet<DailyProgress>(value);
    }

    public IEnumerable<Reminder> Reminders
    {
        get => _reminders;
        set => _reminders = new HashSet<Reminder>(value);
    }

    private Habit() { }

    private Habit(string name, DaysOfWeek dayOfWeek, short dailyGoal, IEnumerable<Reminder> reminders)
    {
        Id = HabitId.Create();
        Name = name;
        Days = dayOfWeek;
        DailyGoal = dailyGoal;
        Progress = new HashSet<DailyProgress>();
        Reminders = reminders;
    }

    public static Habit Create(string name, DaysOfWeek dayOfWeek, short dailyGoal, IEnumerable<Reminder> reminders)
    {
        return new Habit(name, dayOfWeek, dailyGoal, reminders);
    }

    public void AddReminder(Reminder reminder)
    {
        if (!_reminders.Add(reminder))
            throw new ReminderAlreadyExistsException(reminder.Time);
    }

    public void RemoveReminder(TimeSpan time)
    {
        if (_reminders.TryGetValue(Reminder.Create(time), out Reminder reminder))
            _reminders.Remove(reminder);
        else
            throw new ReminderNotFoundException(time);
    }

    public void SetOrCreateProgress(DateTime date, short value)
    {
        if (_progress.TryGetValue(DailyProgress.Create(Id, date), out DailyProgress progress))
        {
            SetProgress(progress, value);
        }
        else
        {
            progress = DailyProgress.Create(Id, date);
            SetProgress(progress, value);
            _progress.Add(progress);
        }

        if (progress.Value == DailyGoal)
            RaiseDomainEvent(new DailyGoalReached(Id, date));
    }

    private void SetProgress(DailyProgress progress, short value)
    {
        if (value > DailyGoal)
            throw new ValueExceededDailyGoalException(progress.Value + value);

        progress.SetValue(value);
    }
}