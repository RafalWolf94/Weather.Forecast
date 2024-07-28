using System.Text.Json;
using System.Text.Json.Serialization;
using Core.Adapters.Out.TechnicalStuff.Json;
using Microsoft.AspNetCore.Http.Json;

namespace Weather.Forecast.DI;

public static class JsonOptionsRegistrations
{
    public static IServiceCollection AddJsonOptions(this IServiceCollection services)
    {
        services.Configure<JsonOptions>(options => { ConfigureJsonSerializerOptions(options.SerializerOptions); });
        return services;
    }

    private static void ConfigureJsonSerializerOptions(JsonSerializerOptions jsonSerializerOptions)
    {
        jsonSerializerOptions.PropertyNameCaseInsensitive = true;
        jsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        jsonSerializerOptions.Converters.Add(new ValueObjectJsonConverterFactory());
        jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        jsonSerializerOptions.Converters.Add(new JsonDateTimeConverter());
    }
}