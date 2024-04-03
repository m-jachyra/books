using System.Security.Claims;
using Backend.Data.Entities;
using Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace Backend.Auth
{
    public class AuthService
    {
        private readonly UserManager<User?> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly JwtManager _jwt;
        
        public AuthService(UserManager<User?> userManager, SignInManager<User> signInManager, JwtManager jwt)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwt = jwt;
        }

        public async Task<bool> RegisterAsync(string email, string password)
        {
            var newUser = new User
            {
                Email = email,
                UserName = email
            };
            
            var result = await _userManager.CreateAsync(newUser, password);

            if (!result.Succeeded) return false;
            
            var user = await _userManager.FindByEmailAsync(email);
            await _userManager.AddToRoleAsync(user!, "Admin");
                
            return true;

        }

        public async Task<TokenDto?> LoginAsync(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, true, lockoutOnFailure: true);

            if (!result.Succeeded) return null;
            
            var user = await _userManager.FindByEmailAsync(email);
            var userRoles = await _userManager.GetRolesAsync(user);

            return await _jwt.GenerateTokens(user, userRoles);
        }

        public async Task<TokenDto?> GenerateAccessTokenFromRefreshToken(ClaimsPrincipal claimsPrincipal, string refreshToken)
        {
            var userId = claimsPrincipal.FindFirst("userid")?.Value;

            if (userId == null) return null;
            
            var user = await _userManager.FindByIdAsync(userId);
            var userRoles = await _userManager.GetRolesAsync(user);
            
            return await _jwt.GenerateAccessTokenFromRefreshToken(user, userRoles, refreshToken);
        }
    }
}