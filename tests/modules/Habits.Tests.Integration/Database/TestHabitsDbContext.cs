using BitHabit.Shared.Tests;
using Habits.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Habits.Tests.Integration.Database;
internal class TestHabitsDbContext : TestDbContext<HabitsContext>
{
    protected override HabitsContext CreateContext(string connectionString)
    {
        var options = new DbContextOptionsBuilder<HabitsContext>()
            .UseSqlServer(connectionString)
            .EnableSensitiveDataLogging()
            .Options;

        return new HabitsContext(options);
    }
}
