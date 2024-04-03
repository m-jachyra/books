using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data.Entities
{
    [PrimaryKey(nameof(UserId), nameof(ReviewId))]
    public class UserReviewPlus(int userId, int reviewId)
    {
        public int UserId { get; set; } = userId;
        public User User { get; set; }
        
        public int ReviewId { get; set; } = reviewId;
        public Review Review { get; set; }
    }
}