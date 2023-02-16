using Habits.Application.Converters;
using Shared.Abstractions.Requests;
using System;
using System.Text.Json.Serialization;

namespace Habits.Application.Commands;

public record CreateHabit : IRequest
{
    public string Name { get; set; }
    public int DaysOfWeek { get; set; }
    public short DailyGoal { get; set; }
    [JsonConverter(typeof(TimeSpanArrayJsonConverter))]
    public TimeSpan[] Reminders { get; set; }
}
