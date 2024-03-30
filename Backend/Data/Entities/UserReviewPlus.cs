using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data.Entities
{
    [PrimaryKey(nameof(UserId), nameof(ReviewId))]
    public class UserReviewPlus
    {
        public int UserId { get; set; }
        public User User { get; set; }
        
        public int ReviewId { get; set; }
        public Review Review { get; set; }
    }
}