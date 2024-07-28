using Core.Domain.Models.ValueObjects;

namespace Core.Domain.TechnicalStuff.Outbox;

public interface IEmailTemplate
{
    string Subject { get; set; }
    string Message { get; set; }
    List<Email> Recipients { get; set; }
}