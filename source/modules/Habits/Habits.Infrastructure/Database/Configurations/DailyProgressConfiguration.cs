using Habits.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Habits.Infrastructure.Database.Configurations;

internal class DailyProgressConfiguration : IEntityTypeConfiguration<DailyProgress>
{
    public void Configure(EntityTypeBuilder<DailyProgress> builder)
    {
        builder.HasKey(p => new { p.Date, p.HabitId });
    }
}
