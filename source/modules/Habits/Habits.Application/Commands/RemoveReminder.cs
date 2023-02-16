using Habits.Application.Converters;
using Shared.Abstractions.Requests;
using System;
using System.Text.Json.Serialization;

namespace Habits.Application.Commands;

public record RemoveReminder(Guid HabitId, [property: JsonConverter(typeof(TimeSpanJsonConverter))] TimeSpan Time) : IRequest;