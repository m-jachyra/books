using Backend.Data.Entities;
using Backend.Models;

namespace Backend.Auth
{
    public interface IJwtManager
    {
        public TokenDto GenerateTokens(User? user);
        public TokenDto GenerateAccessTokenFromRefreshToken(User? user, string refreshToken);
    }
}