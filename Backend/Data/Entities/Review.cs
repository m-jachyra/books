namespace Backend.Data.Entities
{
    public class Review : Entity
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsPositive { get; set; }
        
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}