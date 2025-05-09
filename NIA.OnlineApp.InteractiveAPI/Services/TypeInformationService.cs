using SY.OnlineApp.Data.Entities;
using SY.OnlineApp.Data.Repositories;

namespace SY.OnlineApp.InteractiveAPI.Services
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

        public async Task<IEnumerable<TypeInformation>> GetAllAsync()
        {
            return await _typeRepo.GetAllAsync();
        }

        public async Task<bool> InsertMultipleAsync(IEnumerable<TypeInformation> list)
        {
            try
            {
                await _typeRepo.AddRangeAsync(list);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Insert failed: {ex.Message}");
                throw; 
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
