using SY.OnlineApp.Data.Entities;
using SY.OnlineApp.Repos.Repositories;

namespace SY.OnlineApp.Services.InteractiveServices
{
    public class InteractiveTypeUtilService : IInteractiveTypeUtilService
    {
        private readonly ITypeUtilRepo _repository;

        public InteractiveTypeUtilService(ITypeUtilRepo repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TypeUtil>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<TypeUtil?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> AddAsync(TypeUtil typeUtil)
        {
            try
            {
                await _repository.AddAsync(typeUtil);
                return true;
            }
            catch(Exception ex) 
            {
                Console.WriteLine($"Error inserting TypeUtil: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateAsync(TypeUtil typeUtil)
        {
            try
            {
                await _repository.UpdateAsync(typeUtil);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(TypeUtil typeUtil)
        {
            try
            {
                await _repository.DeleteAsync(typeUtil);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
