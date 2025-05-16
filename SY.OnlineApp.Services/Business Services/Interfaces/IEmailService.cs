using System.Threading.Tasks;

namespace SY.OnlineApp.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendOtpEmail(string toEmail, string subject, string htmlBody);
    }
}
