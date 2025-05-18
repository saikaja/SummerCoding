using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SY.OnlineApp.Models.Models;
using SY.OnlineApp.Repos.Repositories.Interfaces;
using SY.OnlineApp.Services.Business_Services.Interfaces;
using SY.OnlineApp.Services.Interfaces;

namespace SY.OnlineApp.Services.Business_Services
{
    public class PasswordService : IPasswordService
    {
        private readonly IRegisterRepo _repo;
        private readonly IOneTimePassCodeService _otpService;
        private readonly IEmailService _emailService;

        public PasswordService(IRegisterRepo repo, IOneTimePassCodeService otpService, IEmailService emailService)
        {
            _repo = repo;
            _otpService = otpService;
            _emailService = emailService;
        }

        public async Task SendPasswordResetOtpAsync(ForgotPasswordDto dto)
        {
            var user = await _repo.GetByUserNameAsync(dto.UserName);
            if (user == null) throw new KeyNotFoundException("User not found.");

            var otp = await _otpService.GenerateAndSaveOtpAsync(user.Id);
            var emailBody = $@"
                    Hello {dto.UserName}, your one-time passcode is: {otp}. It expires in 5 minutes.

                    You can reset your password by clicking the link below:
                    http://localhost:4200/reset-password

                    Thank you.
                    ";
            await _emailService.SendEmailAsync(user.Email, "Password Reset OTP", emailBody);
        }

        public async Task ResetPasswordAsync(ResetPasswordDto dto)
        {
            if (dto.NewPassword != dto.ConfirmPassword)
                throw new ArgumentException("Passwords do not match.");

            var user = await _repo.GetByUserNameAsync(dto.UserName);
            if (user == null) throw new KeyNotFoundException("User not found.");

            var isValidOtp = await _otpService.ValidateOtpAsync(user.Id, dto.OneTimePassCode);
            if (!isValidOtp) throw new ArgumentException("Invalid or expired OTP.");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            await _repo.SaveAsync();
        }
    }

}
