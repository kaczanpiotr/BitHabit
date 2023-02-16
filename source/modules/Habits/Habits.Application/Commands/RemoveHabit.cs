using Shared.Abstractions.Requests;
using System;

namespace Habits.Application.Commands;

public record RemoveHabit(Guid Id) : IRequest;

