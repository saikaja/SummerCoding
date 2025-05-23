﻿using Microsoft.Extensions.Logging;
using BCrypt.Net;
using SY.OnlineApp.Data.Entities;
using SY.OnlineApp.Models.Dtos;
using SY.OnlineApp.Repos.Repositories.Interfaces;
using SY.OnlineApp.Services.Business_Services.Interfaces;
using SY.OnlineApp.Services.Interfaces;
using SY.OnlineApp.Models.Models;

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

        public async Task<string> RegisterUserAsync(RegisterDto dto)
        {
            try
            {
                if (await _repo.UserNameExistsAsync(dto.UserName))
                    throw new ArgumentException("Username already exists.");

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

                await _repo.AddAsync(register);
                await _repo.SaveAsync();

                var otp = await _otpService.GenerateAndSaveOtpAsync(register.Id);
                var emailBody = $@"
                    Hello {dto.UserName}, your one-time passcode is: {otp}. It expires in 5 minutes.

                    You can reset your password by clicking the link below:
                    http://localhost:4200/set-password

                    Thank you.
                    ";

                await _emailService.SendEmailAsync(dto.Email, "Your One-Time Passcode", emailBody);

                _logger.LogInformation("Registration successful. OTP sent to {Email}", dto.Email);

                return "Registration successful. OTP sent to email.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Registration failed for {Email}", dto.Email);
                throw new ApplicationException("Registration failed. Please try again.", ex);
            }
        }

        public async Task SetPasswordAsync(CreatePasswordDto dto)
        {
            try
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

                _logger.LogInformation("Password set successfully for user {UserName}.", dto.UserName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error setting password for user {UserName}.", dto.UserName);
                throw;
            }
        }
    }
}
