using Habits.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shared.Infrastructure.Database.Configurations;

internal class HabitConfiguration : IEntityTypeConfiguration<Habit>
{
    public void Configure(EntityTypeBuilder<Habit> builder)
    {
        builder.Property(h => h.Id)
            .HasConversion(id => id.Value, id => new HabitId(id));

        builder
            .HasKey(h => h.Id);

        builder
            .HasMany(h => h.Progress)
            .WithOne()
            .HasForeignKey(p => p.HabitId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(h => h.Reminders)
            .WithOne()
            .HasForeignKey(r => r.HabitId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
