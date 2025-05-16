using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Options;
using SY.OnlineApp.Data.Configurations;
using SY.OnlineApp.Services.Interfaces;
using System.Net.Mail;

public class EmailService : IEmailService
{
    private readonly EmailSettings _settings;

    public EmailService(IOptions<EmailSettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_settings.SenderName, _settings.SenderEmail));
        message.To.Add(MailboxAddress.Parse(toEmail));
        message.Subject = subject;
        message.Body = new TextPart("plain") { Text = body };

        using var smtp = new MailKit.Net.Smtp.SmtpClient();  
        await smtp.ConnectAsync(_settings.SmtpServer, _settings.Port, MailKit.Security.SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_settings.Username, _settings.Password);
        await smtp.SendAsync(message);
        await smtp.DisconnectAsync(true);
    }
}
