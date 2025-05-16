using SY.OnlineApp.Models.Dtos;
using SY.OnlineApp.Data.Entities;
using SY.OnlineApp.Repos.Repositories.Interfaces;

namespace SY.OnlineApp.Services.Services
{
    public class RegisterService
    {
        private readonly IRegisterRepo _repo;

        public RegisterService(IRegisterRepo repo)
        {
            _repo = repo;
        }

        public async Task RegisterUserAsync(RegisterDto dto)
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
    }
}
