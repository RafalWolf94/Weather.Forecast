using Core.Domain.Models.ValueObjects;

namespace Core.Domain.TechnicalStuff.Outbox;

public class EmailTemplate : IEmailTemplate
{
    public EmailTemplate(string subject, string message, List<Email> recipients)
    {
        Subject = subject;
        Message = message;
        if (recipients.Count == 0)
            throw new Exception();
        Recipients = recipients;
    }

    public string Subject { get; set; }
    public string Message { get; set; }
    public List<Email> Recipients { get; set; }
}