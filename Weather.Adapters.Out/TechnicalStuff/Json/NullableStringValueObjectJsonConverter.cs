using System.Text.Json;
using System.Text.Json.Serialization;
using Core.Domain.Models.ValueObjects;

namespace Core.Adapters.Out.TechnicalStuff.Json;

public class NullableStringValueObjectJsonConverter<TValueObject> : JsonConverter<TValueObject?>
    where TValueObject : struct, IValueObject<string>
{
    public override TValueObject? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType is JsonTokenType.Null or JsonTokenType.None)
            throw new InvalidOperationException();
        var value = reader.GetString();
        return string.IsNullOrWhiteSpace(value) ? null : new TValueObject { Value = value };
    }

    public override void Write(Utf8JsonWriter writer, TValueObject? nullableValueObject, JsonSerializerOptions options)
    {
        if (nullableValueObject.HasValue)
            writer.WriteStringValue(nullableValueObject.Value.Value);
        else
            writer.WriteNullValue();
    }
}