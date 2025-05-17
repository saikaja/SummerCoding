using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.OnlineApp.Services.Business_Services.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(string userName);
    }
}
