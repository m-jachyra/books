using Microsoft.AspNetCore.Identity;

namespace Backend.Data.Entities
{
    public class User : IdentityUser<int>, IEntity
    {
        public int Id { get; set; }
        
        public ICollection<Review> Reviews { get; } = new List<Review>();
    }
}