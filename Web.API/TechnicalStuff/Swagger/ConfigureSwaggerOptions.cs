using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Weather.Forecast.TechnicalStuff.Swagger;

public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        var securityScheme = new OpenApiSecurityScheme
        {
            Name = "Authorization",
            In = ParameterLocation.Header,
            BearerFormat = "JWT",
        };

        options.AddSecurityDefinition("Bearer", securityScheme);
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer",
                    },
                },
                Array.Empty<string>()
            }
        });
        options.CustomSchemaIds(type =>
        {
            if (type.DeclaringType is not null)
                return $"{type.DeclaringType.Name}.{type.Name}";

            var name = type.GenericTypeArguments.Length > 0 &&
                       type.GenericTypeArguments[0].DeclaringType?.Name is not null
                ? type.GenericTypeArguments[0].DeclaringType?.Name + "."
                : string.Empty;

            return $"{name}{type.Name}";
        });
        options.SchemaFilter<ValueObjectSchemaFilter>();
        options.SupportNonNullableReferenceTypes();
    }

    public void Configure(string? name, SwaggerGenOptions options)
    {
        Configure(options);
    }
}