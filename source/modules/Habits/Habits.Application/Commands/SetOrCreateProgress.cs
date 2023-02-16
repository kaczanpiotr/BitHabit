using Shared.Abstractions.Requests;
using System;

namespace Habits.Application.Commands;

public record SetOrCreateProgress(Guid HabitId, DateTime Date, short Value) : IRequest;