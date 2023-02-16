using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Outbox;

namespace Shared.Infrastructure.Database
{
    public class BitHabitContext : DbContext
    {
        private const string schema = "infra";
        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        public BitHabitContext(DbContextOptions<BitHabitContext> options)
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
            modelBuilder.HasDefaultSchema("infra");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
