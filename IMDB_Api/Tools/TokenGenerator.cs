using IMDB_Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IMDB_Api.Tools
{
    public class TokenGenerator
    {
        private readonly string _secretkey;
        public TokenGenerator(IConfiguration config)
        {
            _secretkey = config.GetSection("TokenInfo").GetSection("secretKey").Value;
        }

        public string GenerateToken(User user)
        {
            //Generation de la Verify Signature
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretkey));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            //Generation du Payload (Data)
            Claim[] myClaims = new[]
            {
                new Claim(ClaimTypes.Role, user.IsAdmin ? "admin" : "user"),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("UserId", user.Id.ToString())
            };

            //Generation du token
            JwtSecurityToken jwt = new JwtSecurityToken(
                claims : myClaims,
                signingCredentials : credentials,
                issuer : "https://monapi.com", //Emetteur du token
                audience : "https://monclient.com", //Consommateur
                expires : DateTime.Now.AddDays(1)
                );

            //Produire le token sous forme de string
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(jwt);

        }
    }
}
