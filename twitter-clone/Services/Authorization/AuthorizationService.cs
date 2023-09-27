using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace twitter_clone.Services.Authorization.IAuthorization
{
    public class AuthorizationService : IAuthorization
    {
        private readonly IConfiguration _configuration;
        public AuthorizationService(IConfiguration configuration)
        {
            _configuration = configuration; 
        }
        public string GetToken(string email, string name)
        {
            DateTime createAt = DateTime.UtcNow;
            var key = _configuration.GetValue<string>("JsonSecret");
            var keyBytes = Encoding.ASCII.GetBytes(key!);

            var claims = new ClaimsIdentity();
            
            claims.AddClaim(new Claim(ClaimTypes.Email, email));
            claims.AddClaim(new Claim(ClaimTypes.Name, name));
            claims.AddClaim(new Claim(ClaimTypes.DateOfBirth, createAt.ToString()));

            var credentialsToken = new SigningCredentials(
                    new SymmetricSecurityKey(keyBytes),
                    SecurityAlgorithms.HmacSha256Signature
                );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = credentialsToken
            }; 

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            string token = tokenHandler.WriteToken(tokenConfig);

            return token; 
        }

        public bool VerifyToken(string token)
        {
            var key = _configuration.GetValue<string>("JsonSecret");
            var keyBytes = Encoding.ASCII.GetBytes(key!);
            var claims = new ClaimsIdentity();
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            try
            {
                tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
