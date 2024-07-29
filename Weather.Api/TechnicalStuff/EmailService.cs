using System.Net;
using System.Net.Mail;
using Core.Domain.TechnicalStuff.Outbox;
using Core.Infrastructure.Services.Outbox;
using Microsoft.Extensions.Options;

namespace Weather.Forecast.TechnicalStuff;

public class EmailService(IOptions<EmailSettings> emailSettings) : IEmailService
{
    public void SendEmail(IEmailTemplate emailTemplate)
    {
        var smtpClient = new SmtpClient(emailSettings.Value.SmtpServer)
        {
            Port = emailSettings.Value.SmtpPort,
            Credentials = new NetworkCredential(emailSettings.Value.SmtpUser, emailSettings.Value.SmtpPass),
            EnableSsl = true,
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(emailSettings.Value.SenderEmail, emailSettings.Value.SenderName),
            Subject = emailTemplate.Subject,
            Body = emailTemplate.Message,
            IsBodyHtml = true,
        };

        foreach (var recipient in emailTemplate.Recipients)
        {
            mailMessage.To.Add(recipient.Value);
        }

        smtpClient.Send(mailMessage);
    }
}