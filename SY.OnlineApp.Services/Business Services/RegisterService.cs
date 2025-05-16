using Microsoft.Extensions.Logging;
using SY.OnlineApp.Data.Entities;
using SY.OnlineApp.Models.Dtos;
using SY.OnlineApp.Repos.Repositories.Interfaces;
using SY.OnlineApp.Services.Business_Services.Interfaces;
using SY.OnlineApp.Services.Interfaces;

namespace SY.OnlineApp.Services.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IRegisterRepo _repo;
        private readonly IOneTimePassCodeService _otpService;
        private readonly ISftpService _sftpService;
        private readonly IEmailService _emailService;
        private readonly ILogger<RegisterService> _logger;

        public RegisterService(
            IRegisterRepo repo,
            IOneTimePassCodeService otpService,
            ISftpService sftpService,
            IEmailService emailService,
            ILogger<RegisterService> logger)
        {
            _repo = repo;
            _otpService = otpService;
            _sftpService = sftpService;
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

                // Save to DB
                await _repo.AddAsync(register);
                await _repo.SaveAsync();

                // Generate OTP and email it
                var otp = await _otpService.GenerateAndSaveOtpAsync(register.Id);
                var template = _sftpService.GetEmailTemplate("/templates/otp_email.html");

                var emailBody = template
                    .Replace("{{OTP}}", otp)
                    .Replace("{{UserName}}", dto.UserName);

                await _emailService.SendOtpEmail(register.Email, "Your One-Time Password", emailBody);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering user: {Email}", dto.Email);
                throw new ApplicationException("Registration failed. Please try again.", ex);
            }
        }
    }
}
