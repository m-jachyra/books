using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Backend.Data.Entities
{
    public class Review : IHasId, ISortable<Review>
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsPositive { get; set; }
        
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<UserReviewPlus> UserReviewPluses { get; } = new List<UserReviewPlus>();

        public static Expression<Func<Review, object>> GetSortProperty(string? sortColumn) =>
            sortColumn?.ToLower() switch
            {
                _ => review => review.Id
            };
    }
}