using Core.Adapters.Out;
using Core.Infrastructure.Services.Outbox;
using Core.UseCases;
using Core.UseCases.TechnicalStuff.Cqrs;
using Core.UseCases.TechnicalStuff.Transactions;
using Weather.Forecast.TechnicalStuff;

namespace Weather.Forecast.DI;

public static class DomainRegistrations
{
    public static IServiceCollection AddDomainModel(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHandlers()
            .AddScoped<ITransactionContext, TransactionContext>()
            // .AddScoped<IQueryHandler<GetAppUser.Query, GetAppUser.Data>, AppUsersQueries>()
            .Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.AddTransient<IEmailService, EmailService>();
        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services
            .Scan(selector => selector.FromAssemblies(
                    AdaptersOutLayerInfo.Assembly,
                    UseCasesLayerInfo.Assembly)
                .AddClasses(filter => filter.AssignableToAny(
                    typeof(ICommandHandler<>),
                    typeof(ICommandHandler<,>),
                    typeof(IQueryHandler<,>)))
                .AsSelfWithInterfaces()
                .WithScopedLifetime());

        // services.Decorate(typeof(ICommandHandler<>), typeof(TransactionCommandHandlerDecorator<>));
        // services.Decorate(typeof(ICommandHandler<,>), typeof(TransactionCommandHandlerDecorator<,>));
        return services;
    }
}