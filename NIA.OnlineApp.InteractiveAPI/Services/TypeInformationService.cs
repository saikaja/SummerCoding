using NIA.OnlineApp.Data.Entities;
using NIA.OnlineApp.Data.Repositories;

namespace NIA.OnlineApp.InteractiveAPI.Services
{
    public class TypeInformationService : ITypeInformationService
    {
        private readonly ITypeInformationRepo _typeRepo;

        public TypeInformationService(ITypeInformationRepo typeRepo)
        {
            _typeRepo = typeRepo;
        }

        public async Task<IEnumerable<TypeInformation>> GetByTypeIdAsync(int Type_Id)
        {
            return await _typeRepo.GetByTypeIdAsync(Type_Id);
        }

        public async Task<bool> InsertMultipleAsync(IEnumerable<TypeInformation> typeInformationList)
        {
            try
            {
                await _typeRepo.AddRangeAsync(typeInformationList);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAttributesAsync(int Type_Id, TypeInformation typeInformation)
        {
            try
            {
                await _typeRepo.UpdateAsync(Type_Id, typeInformation);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAttributesAsync(int Type_Id, TypeInformation typeInformation)
        {
            try
            {
                await _typeRepo.DeleteAsync(Type_Id, typeInformation);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
