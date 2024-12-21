using Lotus_Dashboard1.Areas.Identity.Data;

using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Lotus_Dashboard.Apis.JWT
{
    public class jwtservice:IJwtservice
    {
        public readonly UserManager<Lotus_Dashboard1User> _userrole;

        public jwtservice(UserManager<Lotus_Dashboard1User> userrole)
        {
            _userrole = userrole;
        }
        public async Task<string> generatetoken(Lotus_Dashboard1User user)
        {
            var secretkey = Encoding.UTF8.GetBytes("1234567890asdfgh");
            var signincredential = new SigningCredentials(new SymmetricSecurityKey(secretkey), SecurityAlgorithms.HmacSha256Signature);
            var encriptionkey = Encoding.UTF8.GetBytes("qwsadfrewtyh4532");
            var encriptioncredential = new EncryptingCredentials(new SymmetricSecurityKey(encriptionkey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);
            var roles =_userrole.GetRolesAsync(user).Result.FirstOrDefault();

            //var authClaims = new List<Claim>
            //    {
            //        new Claim(ClaimTypes.Name, user.UserName),
            //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //    };

            
            var tokendescriptor = new SecurityTokenDescriptor()
            
            {
                Issuer =user.UserName.ToString(),
                Audience = roles.ToString(),
                IssuedAt = DateTime.Now,
                NotBefore=DateTime.Now,
                Expires=DateTime.Now.AddDays(1),
                SigningCredentials= signincredential,
                //Subject=new ClaimsIdentity(await Getclaimasync(user)),
                EncryptingCredentials= encriptioncredential,
                //Claims= (IDictionary<string, object>)authClaims


            };
            var tokenhandller = new JwtSecurityTokenHandler();
            var securitytoken = tokenhandller.CreateToken(tokendescriptor);
            return tokenhandller.WriteToken(securitytoken);
            
        }
        public async Task<IEnumerable<Claim>> Getclaimasync(Lotus_Dashboard1User user)
        {
            var claims = new List<Claim>()
            {
                
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),

            };

            var roles = await _userrole.GetRolesAsync(user);
            foreach (var item in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }
            return claims;
        }
    }
}
