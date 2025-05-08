using NIA.OnlineApp.Data.Entities;
using NIA.OnlineApp.Data.Repositories;

namespace NIA.OnlineApp.InteractiveAPI.Services
{
    public class TypeUtilService : ITypeUtilService
    {
        private readonly ITypeUtilRepo _repository;

        public TypeUtilService(ITypeUtilRepo repository)
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
            catch
            {
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
