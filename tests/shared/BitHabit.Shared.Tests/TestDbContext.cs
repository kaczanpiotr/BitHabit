using Microsoft.EntityFrameworkCore;

namespace BitHabit.Shared.Tests;
public abstract class TestDbContext<T> where T : DbContext, IDisposable
{
    public T Context { get; }

    public TestDbContext(string connectionString = null)
    {
        connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BitHabits-test;Integrated Security=True;MultiSubnetFailover=False";

        Context = CreateContext(connectionString);

        if (Context is null)
            throw new InvalidOperationException($"Context failed to create");
    }

    protected abstract T CreateContext(string connectionString);

    public void Dispose()
    {
        Context?.Database.EnsureDeleted();
        Context?.Dispose();
    }
}
