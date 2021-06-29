using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServicioAPISeguridad.Transversal.Common
{
    public static class JwtGenerator
    {
        /// <summary>
        /// Metodo que genera un jwt para su respectiva sesion.
        /// </summary>
        /// <returns></returns>
        public static string CreateToken(IConfiguration _configuration, string pUsername)
        {
            //get the  configuration appseting.json
            var key = _configuration["Jwt:Key"];
            var audienceToken = _configuration["Jwt:AudienceToken"];
            var issuerToken = _configuration["Jwt:IssuerToken"];
            var expireToken = _configuration["Jwt:Expire"];

            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(key));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // create a claimsIdentity
            var claimsIdentity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, pUsername)
            });

            // create token to the user
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: audienceToken,
                issuer: issuerToken,
                subject: claimsIdentity,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(expireToken)),
                signingCredentials: signingCredentials);

            var jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);

            return jwtTokenString;
        }
    }
}
