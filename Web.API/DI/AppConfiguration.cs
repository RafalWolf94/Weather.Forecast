using Weather.Forecast.TechnicalStuff.Error;

namespace Weather.Forecast.DI;

public static class BuildAppExtension
{
    public static void BuildApp(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseExceptionHandler(error => error.UseAppExceptionPolicy());
        app.UseSwaggerExtension();
        app.UseHttpsRedirection();
        app.UseCors(options =>
        {
            options.AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .SetIsOriginAllowed(_ => true);
        });
        app.UseRouting();
        app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.UseAuthentication();
        app.UseAuthorization();
    }
}