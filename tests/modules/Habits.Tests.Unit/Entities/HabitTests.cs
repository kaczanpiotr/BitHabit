using Habits.Domain.Entities;
using Habits.Domain.Enums;
using Habits.Domain.Exceptions;
using Shouldly;
using Xunit;

namespace Habits.Tests.Unit.Entities;

public class HabitTests
{
    [Fact]
    public void given_new_habit_with_uniqe_reminders_should_succeed()
    {
        //arrange
        var firstReminder = Reminder.Create(new TimeSpan(8, 0, 0));
        var secondReminder = Reminder.Create(new TimeSpan(9, 0, 0));

        //act
        var habit =  Habit.Create("habit", DaysOfWeek.All, 10, new[] { firstReminder, secondReminder });

        //assert
        habit.Reminders.ShouldNotBeEmpty();
        habit.Reminders.ShouldBeUnique();
    }

    [Fact]
    public void given_uniqe_reminder_should_succeed()
    {
        var reminder = Reminder.Create(new TimeSpan(8, 0, 0));
        var habit = Habit.Create("habit", DaysOfWeek.All, 10, new[] { reminder });
        var newReminder = Reminder.Create(new TimeSpan(9, 0, 0)); ;

        var exception = Record.Exception(() => habit.AddReminder(newReminder));

        exception.ShouldBeNull();
        habit.Reminders.Count().ShouldBe(2);
    }

    [Fact]
    public void given_existing_reminder_should_fail()
    {
        var reminder = Reminder.Create(new TimeSpan(8, 0, 0));
        var habit = Habit.Create("habit", DaysOfWeek.All, 10, new[] { reminder });
        var newReminder = reminder;

        var exception = Record.Exception(() => habit.AddReminder(newReminder));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ReminderAlreadyExistsException>();
        habit.Reminders.ShouldHaveSingleItem();
    }

    [Fact]
    public void given_existing_reminder_to_remove_should_succeed()
    {
        var reminderTime = new TimeSpan(8, 0, 0);
        var reminder = Reminder.Create(reminderTime);
        var habit = Habit.Create("habit", DaysOfWeek.All, 10, new[] { reminder });

        var exception = Record.Exception(() => habit.RemoveReminder(reminderTime));

        exception.ShouldBeNull();
        habit.Reminders.ShouldBeEmpty();
    }

    [Fact]
    public void given_not_existing_reminder_to_remove_should_fail()
    {
        var reminder = Reminder.Create(new TimeSpan(8, 0, 0));
        var habit = Habit.Create("habit", DaysOfWeek.All, 10, new[] { reminder });
        var notExistingReminderTime = new TimeSpan(9, 0, 0);

        var exception = Record.Exception(() => habit.RemoveReminder(notExistingReminderTime));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ReminderNotFoundException>();
        habit.Reminders.ShouldHaveSingleItem();
    }

    [Fact]
    public void given_valid_daily_progress_should_succeed()
    {
        var habit = Habit.Create("habit", DaysOfWeek.All, 10, Array.Empty<Reminder>());

        var exception = Record.Exception(() => habit.SetOrCreateProgress(new DateTime(2022, 01, 01), 10));

        exception.ShouldBeNull();
        habit.Progress.ShouldHaveSingleItem();
    }

    [Fact]
    public void given_exceeded_daily_progress_should_fail()
    {
        var habit = Habit.Create("habit", DaysOfWeek.All, 10, Array.Empty<Reminder>());

        short progressValue = 11;
        var exception = Record.Exception(() => habit.SetOrCreateProgress(new DateTime(2022, 01, 01), progressValue));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ValueExceededDailyGoalException>();
        habit.Progress.ShouldBeEmpty();
    }
}
