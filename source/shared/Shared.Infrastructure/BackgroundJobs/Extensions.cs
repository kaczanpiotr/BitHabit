using Microsoft.Extensions.Configuration;
using Quartz;
using System;

namespace Shared.Infrastructure.BackgroundJobs;
public static class Extensions
{
    public static void AddJobWithTrigger<T>(
        this IServiceCollectionQuartzConfigurator quartz,
        IConfiguration config)
        where T : IJob
    {
        string jobName = typeof(T).Name;

        var configKey = $"Quartz:{jobName}";
        var cronSchedule = config[configKey];

        if (string.IsNullOrEmpty(cronSchedule))
        {
            throw new Exception($"No Quartz configuration found at {configKey}");
        }

        var jobKey = new JobKey(jobName);
        quartz.AddJob<T>(jobKey)
            .AddTrigger(options => options
                .ForJob(jobKey)
                .WithCronSchedule(cronSchedule)); 
    }
}
