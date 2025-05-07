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
        public Task<bool> DeleteEventAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TypeUtil>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TypeUtil?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertEventAsync(TypeUtil typeUtil)
        {
            throw new NotImplementedException();
        }


        public Task<bool> UpdateEventAsync(TypeUtil typeUtil)
        {
            throw new NotImplementedException();
        }
    }
}
