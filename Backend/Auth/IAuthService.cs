using System.Security.Claims;
using Backend.Data.Entities;
using Backend.Models;

namespace Backend.Auth
{
    public interface IAuthService
    {
        public Task<bool> RegisterAsync(string email, string password);
        public Task<User?> LoginAsync(string email, string password);
        public Task<bool> AddRolesAsync();
        public Task<User?> GetCurrentUserAsync(ClaimsPrincipal user);
    }
}