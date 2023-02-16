using Habits.Application;
using Habits.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Habits.API;
public static class Extensions
{
    public static IServiceCollection AddHabitsModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHabitApplication();
        services.AddHabitInfrastructure(configuration);
        return services;
    }
}
