using System.Threading.Tasks;

namespace SY.OnlineApp.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}
