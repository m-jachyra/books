namespace Backend.Data.Entities
{
    public class Genre : Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Book> Books { get; } = new List<Book>();
    }
}