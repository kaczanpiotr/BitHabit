using Habits.Application.Commands;
using Habits.Application.Commands.Handlers;
using Habits.Application.DTO;
using Habits.Application.Events.Handlers;
using Habits.Application.Queries;
using Habits.Application.Queries.Handlers;
using Habits.Domain.Events;
using Microsoft.Extensions.DependencyInjection;
using Shared.Abstractions.Events;
using Shared.Abstractions.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Habits.Application;

public static class Extensions
{
    public static IServiceCollection AddHabitApplication(this IServiceCollection services)
    {
        services.AddScoped<IRequestHandler<BrowseHabits, IReadOnlyList<HabitDto>>, BrowseHabitsHandler>();
        services.AddScoped<IRequestHandler<CreateHabit, Task>, CreateHabitHandler>();
        services.AddScoped<IRequestHandler<RemoveHabit, Task>, RemoveHabitHandler>();
        services.AddScoped<IRequestHandler<AddReminder, Task>, AddReminderHandler>();
        services.AddScoped<IRequestHandler<RemoveReminder, Task>, RemoveReminderHandler>();
        services.AddScoped<IRequestHandler<SetOrCreateProgress, Task>, SetOrCreateProgressHandler>();

        services.AddScoped<IEventHandler<DailyGoalReached>, DailyGoalReachedHandler>();

        return services;
    }
}