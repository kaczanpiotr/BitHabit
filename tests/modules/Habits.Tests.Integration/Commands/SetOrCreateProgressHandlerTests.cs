using Habits.Application.Commands;
using Habits.Application.Commands.Handlers;
using Habits.Domain.Entities;
using Habits.Domain.Enums;
using Habits.Domain.Events;
using Habits.Domain.Repositories;
using Habits.Infrastructure.Repositories;
using Habits.Tests.Integration.Database;
using Shared.Abstractions.Requests;
using Shouldly;
using Xunit;

namespace Habits.Tests.Integration.Commands;
public class SetOrCreateProgressHandlerTests
{
    [Fact]
    public async Task given_valid_set_or_create_progress_command_should_succeed()
    {
        await _dbContext.Context.Database.EnsureCreatedAsync();
        var habit = Habit.Create("test name", DaysOfWeek.All, 10, Array.Empty<Reminder>());
        await _habitsRepository.AddAsync(habit);

        var command = new SetOrCreateProgress(habit.Id, DateTime.Now, 10);
        await Handle(command);

        var updatedHabit = await _habitsRepository.GetAsync(habit.Id);
        updatedHabit.Progress.ShouldHaveSingleItem();
        updatedHabit.GetDomainEvents().ShouldHaveSingleItem();
        updatedHabit.GetDomainEvents().Single().ShouldBeOfType<DailyGoalReached>();
    }

    private readonly TestHabitsDbContext _dbContext;
    private readonly IHabitsRepository _habitsRepository;
    private readonly IRequestHandler<SetOrCreateProgress> _handler;

    public SetOrCreateProgressHandlerTests()
    {
        _dbContext = new TestHabitsDbContext();
        _habitsRepository = new HabitsRepository(_dbContext.Context);
        _handler = new SetOrCreateProgressHandler(_habitsRepository);
    }

    private async Task Handle(SetOrCreateProgress command) => await _handler.Handle(command, default);
}
