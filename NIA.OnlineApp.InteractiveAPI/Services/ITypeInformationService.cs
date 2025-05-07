using System.ComponentModel;
using NIA.OnlineApp.Data.Entities;

namespace NIA.OnlineApp.InteractiveAPI.Services
{
    public interface ITypeInformationService
    {
        IEnumerable<TypeInformation> GetAllAttributes();
        bool InsertAttributes(TypeInformation typeInformation);
        bool UpdateAttributes(int Type_Id, TypeInformation typeInformation);

        bool DeleteAttributes(int Type_Id, TypeInformation typeInformation);
    }
}
