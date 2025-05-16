using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Crypto.Generators;
using BCrypt.Net;
using SY.OnlineApp.Data.Entities;
using SY.OnlineApp.Models.Dtos;
using SY.OnlineApp.Models.Models;
using SY.OnlineApp.Repos.Repositories.Interfaces;
using SY.OnlineApp.Services.Business_Services.Interfaces;
using SY.OnlineApp.Services.Interfaces;

namespace SY.OnlineApp.Services.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IRegisterRepo _repo;
        private readonly IOneTimePassCodeService _otpService;
        private readonly IEmailService _emailService;
        private readonly ILogger<RegisterService> _logger;

        public RegisterService(
            IRegisterRepo repo,
            IOneTimePassCodeService otpService,
            IEmailService emailService,
            ILogger<RegisterService> logger)
        {
            _repo = repo;
            _otpService = otpService;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task RegisterUserAsync(RegisterDto dto)
        {
            try
            {
                // Check if username is already taken
                if (await _repo.UserNameExistsAsync(dto.UserName))
                    throw new ArgumentException("Username already exists.");

                // Map DTO to entity
                var register = new Register
                {
                    UserName = dto.UserName,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    DateOfBirth = dto.DateOfBirth,
                    AddressLine1 = dto.AddressLine1,
                    AddressLine2 = dto.AddressLine2,
                    City = dto.City,
                    PostalCode = dto.PostalCode
                };

                // Save registration to DB
                await _repo.AddAsync(register);
                await _repo.SaveAsync();

                // Generate OTP and Save it
                var otp = await _otpService.GenerateAndSaveOtpAsync(register.Id);

                // Prepare email body with OTP
                var emailBody = $"Hello {dto.UserName}, your one-time passcode is: {otp}. It expires in 5 minutes.";

                // Send OTP email
                await _emailService.SendEmailAsync(dto.Email, "Your One-Time Passcode", emailBody);

                _logger.LogInformation("Registration successful. OTP sent to {Email}", dto.Email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Registration failed for {Email}", dto.Email);
                throw new ApplicationException("Registration failed. Please try again.", ex);
            }


        }

        public async Task SetPasswordAsync(CreatePasswordDto dto)
        {
            if (dto.NewPassword != dto.ConfirmPassword)
                throw new ArgumentException("Passwords do not match.");

            var user = await _repo.GetByUserNameAsync(dto.UserName);
            if (user == null)
                throw new KeyNotFoundException("User not found.");

            var isValidOtp = await _otpService.ValidateOtpAsync(user.Id, dto.OneTimePassCode);
            if (!isValidOtp)
                throw new ArgumentException("Invalid or expired OTP.");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            user.Status = "Active";

            await _repo.SaveAsync();
        }

    }
}
