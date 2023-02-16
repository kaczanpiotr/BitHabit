using Shared.Abstractions.BuildingBlocks;

namespace Habits.Domain.Entities;

public class HabitId : TypedId
{
    public HabitId(Guid id)
        : base(id)
    {

    }

    public static HabitId Create() => new(Guid.NewGuid());
    public static implicit operator HabitId(Guid value) => new(value);

}