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

        public RegisterService(IRegisterRepo repo, ILogger<RegisterService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task RegisterUserAsync(RegisterDto dto)
        {
            try
            {
                var usernameTaken = await _repo.UserNameExistsAsync(dto.UserName);
                if (usernameTaken)
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
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while registering user: {Username}", dto.UserName);
                throw new ApplicationException("Registration failed. Please try again.", ex);
            }
        }
    }
}
