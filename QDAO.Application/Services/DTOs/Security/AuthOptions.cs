using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace QDAO.Application.Services.DTOs.Security
{
    public class AuthOptions
    {

        public const string ISSUER = "MyAuthServer"; // издатель токена
        public const string AUDIENCE = "MyAuthClient"; // потребитель токена
     
        public const int LIFETIME = 30; // время жизни токена
        public static SymmetricSecurityKey GetSymmetricSecurityKey(string key)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
        }
    }
}
