using Habits.Domain.Enums;
using System;
using System.Text.Json.Serialization;

namespace Habits.Application.DTO
{
    public class HabitDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public short DailyGoal { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DaysOfWeek DaysOfWeek { get; set; }
        public string[] Reminders { get; set; }
        public DailyProgressDto[] Progress { get; set; }
    }
}
