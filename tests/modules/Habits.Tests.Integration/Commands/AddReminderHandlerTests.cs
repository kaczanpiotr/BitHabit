using Habits.Application.Commands;
using Habits.Application.Commands.Handlers;
using Habits.Domain.Entities;
using Habits.Domain.Enums;
using Habits.Domain.Repositories;
using Habits.Infrastructure.Repositories;
using Habits.Tests.Integration.Database;
using Shared.Abstractions.Requests;
using Shouldly;
using Xunit;

namespace Habits.Tests.Integration.Commands;
public class AddReminderHandlerTests
{
    [Fact]
    public async Task given_valid_command_add_reminder_should_succeed()
    {
        await _dbContext.Context.Database.EnsureCreatedAsync();

        var habit = Habit.Create("test name", DaysOfWeek.All, 10, Array.Empty<Reminder>());
        await _habitsRepository.AddAsync(habit);

        var reminderTime = new TimeSpan(8, 0, 0);
        var command = new AddReminder(habit.Id, reminderTime);
        await Handle(command);

        var updatedHabit = await _habitsRepository.GetAsync(habit.Id);
        updatedHabit.Reminders.ShouldHaveSingleItem();
        updatedHabit.Reminders.Single().ShouldBe(Reminder.Create(reminderTime));
    }

    private readonly TestHabitsDbContext _dbContext;
    private readonly IHabitsRepository _habitsRepository;
    private readonly IRequestHandler<AddReminder> _handler;

    public AddReminderHandlerTests()
    {
        _dbContext = new TestHabitsDbContext();
        _habitsRepository = new HabitsRepository(_dbContext.Context);
        _handler = new AddReminderHandler(_habitsRepository);
    }

    private async Task Handle(AddReminder command) => await _handler.Handle(command, default);
}
