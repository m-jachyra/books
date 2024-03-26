namespace Backend.Data.Entities
{
    public class User : Entity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public ICollection<Review> Reviews { get; } = new List<Review>();
    }
}