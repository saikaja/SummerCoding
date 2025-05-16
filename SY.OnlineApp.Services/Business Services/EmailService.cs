using System.Net.Mail;
using System.Net;
using SY.OnlineApp.Services.Interfaces;

public class EmailService : IEmailService
{
    private readonly SmtpClient _smtp;

    public EmailService()
    {
        _smtp = new SmtpClient("smtp.yourhost.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("your@email.com", "password"),
            EnableSsl = true
        };
    }

    public async Task SendOtpEmail(string toEmail, string subject, string htmlBody)
    {
        var message = new MailMessage("your@email.com", toEmail)
        {
            Subject = subject,
            Body = htmlBody,
            IsBodyHtml = true
        };

        await _smtp.SendMailAsync(message);
    }
}
