using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.OnlineApp.Models.Models
{
    public class TokenResponseDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }

    public class RefreshTokenRequest
    {
        public string AccessToken { get; set; }
    }
}
