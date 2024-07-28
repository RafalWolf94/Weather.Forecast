using Core.Domain.Authorization;
using Microsoft.OpenApi.Models;
using Weather.Forecast.TechnicalStuff.Swagger;

namespace Weather.Forecast.DI;

public static class SwaggerConfiguration
{
    public static IServiceCollection AddSwaggerExtension(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JWTSettings"));
        services.AddOpenApi();
        return services;
    }

    private static void AddOpenApi(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "WeatherForecast",
                Version = "1.0",
                Description = "WeatherForecast"
            });
            options.CustomSchemaIds(type =>
            {
                if (type.DeclaringType is not null)
                    return $"{type.DeclaringType.Name}.{type.Name}";

                var name = type.GenericTypeArguments.Length > 0 && type.GenericTypeArguments[0].DeclaringType?.Name is not null
                    ? type.GenericTypeArguments[0].DeclaringType?.Name + "."
                    : string.Empty;

                return $"{name}{type.Name}";
            });
        });

        services.ConfigureOptions<ConfigureSwaggerOptions>();
    }

}