using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Backend.Data.Entities;
using Backend.Models;
using Elastic.Clients.Elasticsearch.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Auth
{
    public class JwtManager
    {
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        
        public JwtManager(IConfiguration configuration, RoleManager<IdentityRole<int>> roleManager)
        {
            _configuration = configuration;
            _roleManager = roleManager;
        }
        
        public async Task<TokenDto> GenerateTokens(User user, IList<string> userRoles)
        {
            return new TokenDto(await GenerateAccessToken(user, userRoles), GenerateRefreshToken());
        }
        
        public async Task<TokenDto> GenerateAccessTokenFromRefreshToken(User user, IList<string> userRoles, string refreshToken)
        {
            return new TokenDto(await GenerateAccessToken(user, userRoles), refreshToken);
        }

        private async Task<string> GenerateAccessToken(User user, IList<string> userRoles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Key"] ?? throw new InvalidOperationException());
            var claims = await GetClaims(user, userRoles);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(15),
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        
        private string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }
        
        private async Task<List<Claim>> GetClaims(User user, IList<string> userRoles)
        {
            var identityOptions = new IdentityOptions();
            var claims = new List<Claim>
            {
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new (JwtRegisteredClaimNames.Sub, user.UserName),
                new (JwtRegisteredClaimNames.Email, user.Email),
                new (identityOptions.ClaimsIdentity.UserIdClaimType, user.Id.ToString()),
                new (identityOptions.ClaimsIdentity.UserNameClaimType, user.UserName)
            };
            
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
                var role = await _roleManager.FindByNameAsync(userRole);
                if(role != null)
                {
                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    foreach(Claim roleClaim in roleClaims)
                    {
                        claims.Add(roleClaim);
                    }
                }
            }

            return claims;
        }
    }
}