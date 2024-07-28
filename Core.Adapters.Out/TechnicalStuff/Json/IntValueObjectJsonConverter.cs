using System.Text.Json;
using System.Text.Json.Serialization;
using Core.Domain.Models.ValueObjects;

namespace Core.Adapters.Out.TechnicalStuff.Json;

public class IntValueObjectJsonConverter<TValueObject> : JsonConverter<TValueObject>
    where TValueObject : IValueObject<int>, new()
{
    public override TValueObject Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType is JsonTokenType.Null or JsonTokenType.None)
            throw new InvalidOperationException();
        return new TValueObject { Value = reader.GetInt32() };
    }

    public override void Write(Utf8JsonWriter writer, TValueObject valueObject, JsonSerializerOptions options) =>
        writer.WriteNumberValue(valueObject.Value);
}