using System.Text.Json.Serialization;
using Core.Domain.Authorization;
using Weather.Forecast.TechnicalStuff.Authorization;

namespace Weather.Forecast.DI;

public static class ApiConfiguration
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddControllers();
        services.Configure<JwtSettings>(configuration.GetSection("JWTSettings"));
        services.AddTransient<ITokenService, TokenService>();
        services.AddAuthentication();
        services.AddControllers().AddJsonOptions(x =>
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

        return services;
    }

    public static void UseSwaggerExtension(this IApplicationBuilder builder)
    {
        builder.UseSwagger();

        builder.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi.Api"); });
    }
}