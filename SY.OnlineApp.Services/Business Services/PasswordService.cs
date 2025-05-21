using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SY.OnlineApp.Data;
using SY.OnlineApp.Data.Entities;
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
        private readonly ILogger<PasswordService> _logger;
        private readonly BusinessDbContext _context;

        public PasswordService(
            IRegisterRepo repo,
            IOneTimePassCodeService otpService,
            IEmailService emailService,
            ILogger<PasswordService> logger,
            BusinessDbContext context)
        {
            _repo = repo;
            _otpService = otpService;
            _emailService = emailService;
            _logger = logger;
            _context = context;
        }

        public async Task SendPasswordResetOtpAsync(ForgotPasswordDto dto)
        {
            try
            {
                var user = await _repo.GetByUserNameAsync(dto.UserName);
                if (user == null)
                {
                    _logger.LogWarning("User not found: {UserName}", dto.UserName);
                    return;
                }

                var otp = await _otpService.GenerateAndSaveOtpAsync(user.Id);
                var emailBody = $@"
                    Hello {dto.UserName}, your one-time passcode is: {otp}. It expires in 5 minutes.

                    You can reset your password by clicking the link below:
                    http://localhost:4200/reset-password

                    Thank you.
                    ";

                await _emailService.SendEmailAsync(user.Email, "Password Reset OTP", emailBody);
                _logger.LogInformation("Password reset OTP sent to {UserEmail}.", user.Email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send password reset OTP for {UserName}.", dto.UserName);
            }
        }

        public async Task ResetPasswordAsync(ResetPasswordDto dto)
        {
            try
            {
                if (dto.NewPassword != dto.ConfirmPassword)
                {
                    _logger.LogWarning("Password mismatch for user {UserName}.", dto.UserName);
                    return;
                }

                var user = await _repo.GetByUserNameAsync(dto.UserName);
                if (user == null)
                {
                    _logger.LogWarning("User not found: {UserName}.", dto.UserName);
                    return;
                }

                var isValidOtp = await _otpService.ValidateOtpAsync(user.Id, dto.OneTimePassCode);
                if (!isValidOtp)
                {
                    _logger.LogWarning("Invalid or expired OTP for user {UserName}.", dto.UserName);
                    return;
                }

                // Check if new password was used recently
                var lastTwo = await _context.PasswordHistories
                    .Where(ph => ph.RegistrationId == user.Id)
                    .OrderByDescending(ph => ph.ChangedAt)
                    .Take(2)
                    .ToListAsync();

                if (lastTwo.Any(ph => BCrypt.Net.BCrypt.Verify(dto.NewPassword, ph.PasswordHash)))
                {
                    _logger.LogWarning("Password reuse attempt for user {UserName}.", dto.UserName);
                    return;
                }

                // Save current password to history
                await _context.PasswordHistories.AddAsync(new PasswordHistory
                {
                    RegistrationId = user.Id,
                    PasswordHash = user.PasswordHash,
                    ChangedAt = DateTime.UtcNow
                });

                // Update to new password
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
                await _repo.SaveAsync();

                _logger.LogInformation("Password reset successfully for user {UserName}.", dto.UserName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to reset password for {UserName}.", dto.UserName);
            }
        }
    }
}
