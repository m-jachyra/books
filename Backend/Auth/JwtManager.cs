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
    public class JwtManager : IJwtManager
    {
        private readonly IConfiguration _configuration;
        
        public JwtManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public TokenDto GenerateTokens(User? user)
        {
            return new TokenDto(GenerateAccessToken(user), GenerateRefreshToken());
        }
        
        public TokenDto GenerateAccessTokenFromRefreshToken(User? user, string refreshToken)
        {
            return new TokenDto(GenerateAccessToken(user), refreshToken);
        }

        private string GenerateAccessToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Key"] ?? throw new InvalidOperationException());
            var claims = new List<Claim>
            {
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new (JwtRegisteredClaimNames.Sub, user.Email),
                new (JwtRegisteredClaimNames.Email, user.Email),
                new ("userid", user.Id.ToString())
            };
            
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
    }
}