using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Habits.Application.Converters;

public class TimeSpanArrayJsonConverter : JsonConverter<TimeSpan[]>
{
    public override TimeSpan[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException();
        }

        List<TimeSpan> list = new List<TimeSpan>();

        reader.Read();

        while (reader.TokenType != JsonTokenType.EndArray)
        {
            var x = reader.GetString();
            list.Add(TimeSpan.Parse(x));

            reader.Read();
        }

        return list.ToArray();
    }

    public override void Write(Utf8JsonWriter writer, TimeSpan[] value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
