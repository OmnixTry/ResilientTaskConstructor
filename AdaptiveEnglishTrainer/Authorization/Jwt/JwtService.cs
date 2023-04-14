using AdaptiveEnglishTrainer.Authorization.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AdaptiveEnglishTrainer.Authorization.Jwt
{
    public class JwtService
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _jwtConfig;
        private readonly string _jwtName = "JwtSettings";
        private readonly string _jwtKeyName = "securityKey";
        private readonly UserManager<User> _userManager;

        public JwtService(IConfiguration configuration, UserManager<User> userManager)
        {
            _configuration = configuration;
            _jwtConfig = configuration.GetSection(_jwtName);
            _userManager = userManager;
        }

        public SigningCredentials GetCredentials()
        {
            var section = _jwtConfig.GetSection(_jwtKeyName).Value;
            var key = Encoding.UTF8.GetBytes(section);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        public List<Claim> GetClaims(User identityUser)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, identityUser.Email)
            };

            var roles = _userManager.GetRolesAsync(identityUser).Result;
            foreach (var item in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }

            return claims;
        }

        public JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var issuer = _jwtConfig.GetSection("validIssuer").Value;
            var audience = _jwtConfig.GetSection("validAudience").Value;
            var expiration = DateTime.Now.AddMinutes(Convert.ToDouble(_jwtConfig.GetSection("expiresIn").Value));
            var tokenOptions = new JwtSecurityToken(issuer: issuer, audience: audience, claims: claims, 
                expires: expiration, signingCredentials: signingCredentials);

            return tokenOptions;
        }
    }
}
