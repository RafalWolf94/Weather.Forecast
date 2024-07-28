using Core.Domain.TechnicalStuff.Outbox;

namespace Weather.Forecast.TechnicalStuff;

public interface IEmailService
{
    void SendEmail(IEmailTemplate emailTemplate);
}