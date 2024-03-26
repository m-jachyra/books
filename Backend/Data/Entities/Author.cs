namespace Backend.Data.Entities
{
    public class Author : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public DateTime? DateBirth { get; set; }
        public DateTime? DateDeath { get; set; }

        public ICollection<Book> Books { get; } = new List<Book>();
    }
}