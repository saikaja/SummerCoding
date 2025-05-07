using Microsoft.Win32;
using NIA.OnlineApp.Data.Entities;

namespace NIA.OnlineApp.InteractiveAPI.Services
{
    public interface ITypeUtilService
    {
        IEnumerable<TypeUtil> GetAllEvents();
        bool InsertEvent(TypeUtil typeUtil);
        bool UpdateEvent(int Id, TypeUtil typeUtil);

        bool DeleteAttributes(int Id, TypeUtil typeUtil);
    }
}
