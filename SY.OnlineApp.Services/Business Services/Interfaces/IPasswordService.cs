using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SY.OnlineApp.Models.Models;

namespace SY.OnlineApp.Services.Business_Services.Interfaces
{
    public interface IPasswordService
    {
        Task SendPasswordResetOtpAsync(ForgotPasswordDto dto);
        Task ResetPasswordAsync(ResetPasswordDto dto);
        Task<bool> ValidateOtpAsync(int registrationId, string code);
    }

}
