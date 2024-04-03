using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Backend.Data;
using Backend.Data.Entities;
using Backend.Models;
using Elastic.Clients.Elasticsearch.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Auth
{
    public class JwtManager
    {
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly AppDbContext _context;
        
        public JwtManager(IConfiguration configuration, RoleManager<IdentityRole<int>> roleManager, AppDbContext context)
        {
            _configuration = configuration;
            _roleManager = roleManager;
            _context = context;
        }
        
        public async Task<TokenDto> GenerateTokensAsync(User user, IList<string> userRoles)
        {
            return new TokenDto(await GenerateAccessToken(user, userRoles), await GenerateRefreshToken(user));
        }
        
        public async Task<TokenDto?> GenerateAccessTokenFromRefreshTokenAsync(User user, IList<string> userRoles, string refreshToken)
        {
            var token = await _context.UserRefreshTokens.FirstOrDefaultAsync(x => x.UserId == user.Id && x.DateExpireUtc > DateTime.UtcNow);

            return token == null ? null : new TokenDto(await GenerateAccessToken(user, userRoles), refreshToken);
        }

        public async Task RemoveRefreshTokenAsync(User user)
        {
            var token = await _context.UserRefreshTokens.FirstOrDefaultAsync(x => x.UserId == user.Id && x.DateExpireUtc > DateTime.UtcNow);
            _context.UserRefreshTokens.Remove(token);
            await _context.SaveChangesAsync();
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
        
        private async Task<string> GenerateRefreshToken(User user)
        {
            var token = await _context.UserRefreshTokens.FirstOrDefaultAsync(x => x.UserId == user.Id);

            if (token != null) return token.RefreshToken;
            
            var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
            
            var userRefreshToken = new UserRefreshToken
            {
                UserId = user.Id,
                RefreshToken = refreshToken,
                DateCreatedUtc = DateTime.UtcNow,
                DateExpireUtc = DateTime.UtcNow.AddDays(7)
            };

            await _context.UserRefreshTokens.AddAsync(userRefreshToken);
            await _context.SaveChangesAsync();

            return refreshToken;
        }
        
        private async Task<List<Claim>> GetClaims(User user, IList<string> userRoles)
        {
            var identityOptions = new IdentityOptions();
            var claims = new List<Claim>
            {
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new (JwtRegisteredClaimNames.Sub, user.Id.ToString()),
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