using Core.Domain.Models.ValueObjects;
using JetBrains.Annotations;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Weather.Forecast.TechnicalStuff.Swagger;

[UsedImplicitly]
public class ValueObjectSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (!IsValueObject(context.Type, out var valueType)) return;
        if (valueType is null) return;
        schema.Type = CreateTypeFrom(valueType);
        schema.Properties.Clear();
    }

    private static string CreateTypeFrom(Type valueType)
    {
        if (valueType == typeof(string)) return "string";
        if (valueType == typeof(int)) return "integer";
        if (valueType == typeof(long)) return "integer";
        if (valueType == typeof(decimal)) return "decimal";
        if (valueType == typeof(float)) return "float";
        if (valueType == typeof(double)) return "double";
        
        return valueType == typeof(bool) ? "boolean": "object";
    }

    private static bool IsValueObject(Type type, out Type? valueType)
    {
        valueType = type.GetInterfaces()
            .SingleOrDefault(t => t.IsGenericType &&
                                  t.GetGenericTypeDefinition() == typeof(IValueObject<>))
            ?.GetGenericArguments()[0];
        return valueType != null;
    }
}