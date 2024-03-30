using System.ComponentModel.DataAnnotations;

namespace Backend.Data.Entities
{
    public class UserRefreshToken : IHasId
    {
        public int Id { get; set; }
        [MaxLength(64)]
        public required string RefreshToken { get; set; }
        public required DateTime DateCreatedUtc { get; set; } = DateTime.UtcNow;
        public required DateTime DateExpireUtc { get; set; }
        
        public int UserId { get; set; }
        public User User { get; set; }
    }
}