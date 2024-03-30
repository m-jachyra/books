using Microsoft.AspNetCore.Identity;

namespace Backend.Data.Entities
{
    public class User : IdentityUser<int>, IHasId
    {
        public ICollection<Review> Reviews { get; } = new List<Review>();
        public ICollection<UserRefreshToken> RefreshTokens { get; } = new List<UserRefreshToken>();
        public ICollection<UserReviewPlus> UserReviewPluses { get; } = new List<UserReviewPlus>();
    }
}