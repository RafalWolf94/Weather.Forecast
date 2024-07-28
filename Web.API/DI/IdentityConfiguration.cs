using System.Text;
using Core.Domain.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Weather.Forecast.TechnicalStuff.Authorization;

namespace Weather.Forecast.DI;

public static class IdentityConfiguration
{
    public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();

        services.AddAuthentication(SetupAuthentication)
            .AddJwtBearer(options => SetupJwtBearer(options, configuration));

        services.AddTransient<IAuthenticatedUserService, AuthenticatedUserService>();
    }

    private static void SetupAuthentication(AuthenticationOptions options)
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }

    private static void SetupJwtBearer(JwtBearerOptions options, IConfiguration configuration)
    {
        options.RequireHttpsMetadata = false;

        options.SaveToken = false;
        var jwtKey = configuration["JWTSettings:Key"];
        if (string.IsNullOrWhiteSpace(jwtKey)) return;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = configuration["JWTSettings:Issuer"],
            ValidAudience = configuration["JWTSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                context.NoResult();
                context.Response.StatusCode = 400;
                context.Response.ContentType = "text/plain";

                return context.Response.WriteAsync(context.Exception.ToString());
            },
            OnChallenge = context =>
            {
                context.HandleResponse();
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";

                return context.Response.WriteAsync(string.Empty);
            },
            OnForbidden = context =>
            {
                context.Response.StatusCode = 403;
                context.Response.ContentType = "application/json";

                return context.Response.WriteAsync(string.Empty);
            }
        };
    }
}