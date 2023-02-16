using Habits.Application.DTO;
using Shared.Abstractions.Requests;
using System.Collections.Generic;

namespace Habits.Application.Queries;

public record BrowseHabits : IRequest<IReadOnlyList<HabitDto>>;
