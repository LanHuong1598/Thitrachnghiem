using Microsoft.IdentityModel.Tokens;
using Thitrachnghiem.Commons;
using Thitrachnghiem.Users.Models.Entities;
using Thitrachnghiem.Users.Models.Functions;
using Thitrachnghiem.Users.Models.Schema;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Thitrachnghiem.Services
{
    public static class TokenService
    {
        private const int EXPIRE_YEARS = 1;
        public static string CreateToken(UserGet user)
        {
            F_Users f_users = new F_Users();

            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenHandler = new JwtSecurityTokenHandler();

            ClaimsIdentity getClaimsIdentity()
            {
                return new ClaimsIdentity(
                    getClaims()
                    );

                Claim[] getClaims()
                {
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, user.Username.ToString()));
                    claims.Add(new Claim(ClaimTypes.Sid, user.Uuid.ToString()));         

                    foreach (var item in new F_Userrole().GetRoleClaimById(user.Id).Role)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, item));
                    }
                    return claims.ToArray();
                }

            }


            var descriptor = new SecurityTokenDescriptor
            {
                Subject = getClaimsIdentity(),
                Expires = DateTime.UtcNow.AddYears(EXPIRE_YEARS),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
