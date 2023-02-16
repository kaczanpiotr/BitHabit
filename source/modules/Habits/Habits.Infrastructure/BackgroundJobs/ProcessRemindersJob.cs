using Habits.Domain.Enums;
using Habits.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Habits.Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public class ProcessRemindersJob : IJob
{
    private readonly IHabitsRepository _habitsRepository;
    protected readonly ILogger<ProcessRemindersJob> _logger;
    public ProcessRemindersJob(IHabitsRepository habitsRepository, ILogger<ProcessRemindersJob> logger)
    {
        _habitsRepository = habitsRepository;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var currentDateTime = DateTime.UtcNow;
        var currentTime = new TimeSpan(currentDateTime.Hour, currentDateTime.Minute, 0);
        var currentDayOfWeekAsInteger = (int)currentDateTime.DayOfWeek;

        var habitsOfCurrentDay = await _habitsRepository.BrowseAsync(h => h.Days.HasFlag(DaysOfWeekMap[currentDayOfWeekAsInteger]));

        if (habitsOfCurrentDay.Any())
        {
            var habitsToRemind = habitsOfCurrentDay.Where(h => h.Reminders.Any(r => r.Time.Equals(currentTime)));

            foreach (var habit in habitsToRemind)
            {
                _logger.LogInformation($"Reminder of {habit.Name} at {currentTime}");
            }
        }
    }

    private static DaysOfWeek[] DaysOfWeekMap =>
        new[]
        {
                DaysOfWeek.Sunday,
                DaysOfWeek.Monday,
                DaysOfWeek.Tuesday,
                DaysOfWeek.Wednesday,
                DaysOfWeek.Thursday,
                DaysOfWeek.Friday,
                DaysOfWeek.Saturday,
        };
}
