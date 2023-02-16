using Habits.Domain.Repositories;
using Habits.Infrastructure.BackgroundJobs;
using Habits.Infrastructure.Database;
using Habits.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Shared.Infrastructure.BackgroundJobs;

namespace Habits.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddHabitInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<HabitsContext>(options => options.UseSqlServer(configuration.GetConnectionString("HabitsDatabase")));

        services.AddScoped<IHabitsRepository, HabitsRepository>();

        services.AddQuartz(configure =>
        {
            configure.AddJobWithTrigger<ProcessRemindersJob>(configuration);

            configure.UseMicrosoftDependencyInjectionJobFactory();
        });


        return services;
    }

    public static IApplicationBuilder UseHabitInfrastructure(this IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetRequiredService<HabitsContext>();
            context.Database.Migrate();
        }

        return app;
    }
}