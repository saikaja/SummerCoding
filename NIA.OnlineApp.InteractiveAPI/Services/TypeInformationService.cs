using NIA.OnlineApp.Data.Entities;
using NIA.OnlineApp.Data.Repositories;

namespace NIA.OnlineApp.InteractiveAPI.Services
{
    public class TypeInformationService : ITypeInformationService
    {
        private readonly ITypeInformationRepo _repository;

        public TypeInformationService (ITypeInformationRepo repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TypeInformation>> GetByTypeIdAsync(int typeId)
        {
            return await _repository.GetByTypeIdAsync(typeId);
        }

        public bool DeleteAttributes(int Type_Id, TypeInformation typeInformation)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TypeInformation> GetAllAttributes()
        {
            throw new NotImplementedException();
        }

        public bool InsertAttributes(TypeInformation typeInformation)
        {
            throw new NotImplementedException();
        }

        public bool UpdateAttributes(int Type_Id, TypeInformation typeInformation)
        {
            throw new NotImplementedException();
        }
    }
}
