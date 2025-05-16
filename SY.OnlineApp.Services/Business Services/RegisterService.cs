using SY.OnlineApp.Models.Dtos;
using SY.OnlineApp.Data.Entities;
using SY.OnlineApp.Repos.Repositories.Interfaces;
using SY.OnlineApp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace SY.OnlineApp.Services.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IRegisterRepo _repo;
        private readonly ILogger<RegisterService> _logger;
        private readonly EmailService _emailService;
        private readonly SftpService _sftpService;

        public RegisterService(IRegisterRepo repo, ILogger<RegisterService> logger, EmailService emailService, SftpService sftpService)
        {
            _repo = repo;
            _logger = logger;
            _emailService = emailService;
            _sftpService = sftpService;
        }

        public async Task RegisterUserAsync(RegisterDto dto)
        {
            try
            {
                if (await _repo.UserNameExistsAsync(dto.UserName))
                    throw new ArgumentException("Username already exists.");

                var otp = OtpGenerator.GenerateOtp();
                var template = _sftpService.GetEmailTemplate("/otp_templates/otp_email.html");
                var body = template.Replace("{{OTP}}", otp);

                await _emailService.SendOtpEmail(dto.Email, "Your OTP Code", body);

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
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during registration for {Email}", dto.Email);
                throw new ApplicationException("Registration failed.", ex);
            }
        }
    }
}
