namespace Weather.Forecast.DI;

public static class DataBaseConfiguration
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        // services.AddDbContext<AppDbContext>(x =>
        // {
        //     x.EnableDetailedErrors();
        //     x.EnableSensitiveDataLogging();
        //     x.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        // });
        //
        // services.AddScoped<IDbContext, AppDbContext>();
        return services;
    }
}