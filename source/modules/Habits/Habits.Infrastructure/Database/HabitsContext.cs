using Habits.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Habits.Infrastructure.Database;

public class HabitsContext : DbContext
{
    private const string schema = "habits";
    public DbSet<Habit> Habits { get; set; }

    public HabitsContext(DbContextOptions<HabitsContext> options)
        : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(options => options.MigrationsHistoryTable("_EFMigrationsHistory", schema));
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(schema);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
