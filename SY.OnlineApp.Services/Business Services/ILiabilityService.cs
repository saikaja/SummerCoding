using SY.OnlineApp.Data.Entities;
using SY.OnlineApp.Models.Models;

namespace SY.OnlineApp.Services.Business_Services
{
    public interface ILiabilityService
    {
        Task<List<TypeInformationDto>> GetLiabilitiesAsync();
    }

}
