using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Shared.Abstractions.Dispatchers;
using Shared.Infrastructure.BackgroundJobs;
using Shared.Infrastructure.Database;
using Shared.Infrastructure.Database.Interceptors;
using Shared.Infrastructure.Dispatchers;
using Shared.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;


namespace Shared.Infrastructure;
public static class Extensions
{
    public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IDispatcher, Dispatcher>();
        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

        services.AddDbContext<BitHabitContext>((serviceProvider, options) =>
        {
            var interceptor = serviceProvider.GetRequiredService<ConvertDomainEventsToOutboxMessagesInterceptor>();

            options
                .UseSqlServer(configuration.GetConnectionString("HabitsDatabase"))
                .AddInterceptors(interceptor);
        });

        services.AddScoped<ExceptionMiddleware>();

        services.AddQuartz(configure =>
        {
            configure.AddJobWithTrigger<ProcessOutboxMessagesJob>(configuration);
            configure.UseMicrosoftDependencyInjectionJobFactory();
        });

        services.AddQuartzHostedService();

        return services;
    }

    public static IApplicationBuilder UseSharedInfrastructure(this IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetRequiredService<BitHabitContext>();
            context.Database.Migrate();
        }

        app.UseMiddleware<ExceptionMiddleware>();
        return app;
    }
}
