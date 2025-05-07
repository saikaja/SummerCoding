using NIA.OnlineApp.Data.Entities;

namespace NIA.OnlineApp.InteractiveAPI.Services
{
    public class TypeInformationService : ITypeInformationService
    {
        private readonly ITypeInformationService _repository;

        public TypeInformationService (ITypeInformationService repository)
        {
            _repository = repository;
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
