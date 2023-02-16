using System;

namespace Habits.Application.DTO;
public class DailyProgressDto
{
    public Guid HabitId { get; set; }
    public DateTime Date { get; set; }
    public short Value { get; set; }
}
