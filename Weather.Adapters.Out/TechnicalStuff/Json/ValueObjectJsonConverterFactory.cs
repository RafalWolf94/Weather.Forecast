using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Core.Domain.Models.ValueObjects;

namespace Core.Adapters.Out.TechnicalStuff.Json;

public class ValueObjectJsonConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        if (IsNullable(typeToConvert, out var innerType))
            return IsValueObject(innerType, out var valueType) &&
                   IsValueTypeSupportForNullable(valueType);
        else
            return IsValueObject(typeToConvert, out var valueType) &&
                   IsValueTypeSupportForNotNullable(valueType);
    }


    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        => IsNullable(typeToConvert, out var innerType)
            ? CreateConverterForNullable(innerType)
            : CreateConverterForNotNullable(typeToConvert);

    private static JsonConverter CreateConverterForNullable(Type typeToConvert)
    {
        if (!IsValueObject(typeToConvert, out var valueType))
            throw new NotSupportedException();
        if (valueType == typeof(string))
            return (JsonConverter)Activator.CreateInstance(
                typeof(NullableStringValueObjectJsonConverter<>).MakeGenericType(typeToConvert))!;


        throw new NotSupportedException();
    }

    private static JsonConverter CreateConverterForNotNullable(Type typeToConvert)
    {
        if (!IsValueObject(typeToConvert, out var valueType))
            throw new NotSupportedException();
        if (valueType == typeof(string))
            return (JsonConverter)Activator.CreateInstance(
                typeof(StringValueObjectJsonConverter<>).MakeGenericType(typeToConvert))!;
        if (valueType == typeof(int))
            return (JsonConverter)Activator.CreateInstance(
                typeof(IntValueObjectJsonConverter<>).MakeGenericType(typeToConvert))!;

        throw new NotSupportedException();
    }

    private static bool IsNullable(Type type, [NotNullWhen(true)] out Type? innerType)
    {
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            innerType = type.GenericTypeArguments[0];
            return true;
        }

        innerType = null;
        return false;
    }

    private static bool IsValueObject(Type type, [NotNullWhen(true)] out Type? valueType)
    {
        valueType = type
            .GetInterfaces()
            .SingleOrDefault(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IValueObject<>))
            ?.GetGenericArguments()[0];

        return valueType != null;
    }

    private static bool IsValueTypeSupportForNullable(Type valueType) =>
        valueType == typeof(string);

    private static bool IsValueTypeSupportForNotNullable(Type valueType) =>
        valueType == typeof(string) ||
        valueType == typeof(int) ||
        valueType == typeof(long);
}