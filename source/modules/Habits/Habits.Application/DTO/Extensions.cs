using Habits.Domain.Entities;
using System.Linq;

namespace Habits.Application.DTO;
internal static class Extensions
{
    public static HabitDto AsDto(this Habit habit)
    {
        return new HabitDto()
        {
            Id = habit.Id.Value,
            Name = habit.Name,
            DailyGoal = habit.DailyGoal,
            DaysOfWeek = habit.Days,
            Reminders = habit.Reminders.Select(x => x.Time.ToString(@"hh\:mm")).ToArray(),
            Progress = habit.Progress.Select(p => p.AsDto()).ToArray()
        };
    }

    public static DailyProgressDto AsDto(this DailyProgress progress)
    {
        return new DailyProgressDto()
        {
            HabitId = progress.HabitId.Value,
            Date = progress.Date,
            Value = progress.Value,
        };
    }
}
