using Core.Presentation;
using Serilog;
using Weather.Forecast.DI;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services
    .AddJsonOptions()
    .AddPersistence(builder.Configuration)
    .AddDomainModel(builder.Configuration)
    .AddServices(builder.Configuration)
    .AddProblemDetails()
    .AddSwaggerExtension(builder.Configuration)
    .AddAuthentication(builder.Configuration);

var app = builder.Build();
app.UseSerilogRequestLogging();
app.MapEndpoints();
app.BuildApp();
app.Run();