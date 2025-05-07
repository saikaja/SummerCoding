using NIA.OnlineApp.Data.Entities;

namespace NIA.OnlineApp.InteractiveAPI.Services
{
    public class TypeUtilService : ITypeUtilService
    {
        private readonly ITypeUtilService _repository;
        public TypeUtilService(ITypeUtilService repository)
        {
            _repository = repository;
        }
        public bool DeleteAttributes(int Id, TypeUtil typeUtil)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TypeUtil> GetAllEvents()
        {
            throw new NotImplementedException();
        }

        public bool InsertEvent(TypeUtil typeUtil)
        {
            throw new NotImplementedException();
        }

        public bool UpdateEvent(int Id, TypeUtil typeUtil)
        {
            throw new NotImplementedException();
        }
    }
}
