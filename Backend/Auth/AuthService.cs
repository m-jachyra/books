using System.Security.Claims;
using Backend.Data.Entities;
using Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace Backend.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User?> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        
        public AuthService(UserManager<User?> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
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

        public async Task<User?> LoginAsync(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, true, lockoutOnFailure: true);
            
            if (result.Succeeded)
                return await _userManager.FindByEmailAsync(email);

            return null;
        }

        public async Task<User?> GetCurrentUserAsync(ClaimsPrincipal user)
        {
            var userId = user.FindFirst("userid")?.Value;

            if (userId == null) return null;
            
            return await _userManager.FindByIdAsync(userId);
        }
        
        public async Task<bool> AddRolesAsync()
        {
            var roles = new[] { "Admin", "User" };

            foreach (var role in roles)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole<int>(role));

                if (!result.Succeeded) return false;
            }

            return true;
        }
    }
}