using QDAO.Persistence.Repositories.User;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using QDAO.Application.Services.DTOs.Security;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Application.Services
{
    public class SecurityService
    {
        private readonly UserRepository _userRepository;

        public SecurityService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> GetAccessToken(long userId, short userRole, CancellationToken ct)
        {
           
            var userIdentity = new ClaimsIdentity();
            userIdentity.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, userId.ToString()));
            userIdentity.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, userRole.ToString()));

            var encodedJwt = CreateJwt(userIdentity);

            return encodedJwt;
        }

        private string CreateJwt(ClaimsIdentity userIdentity)
        {
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: DateTime.UtcNow,
                    claims: userIdentity.Claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}
