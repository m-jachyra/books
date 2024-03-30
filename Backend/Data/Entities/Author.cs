using System.ComponentModel.DataAnnotations;

namespace Backend.Data.Entities
{
    public class Author : IHasId
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        public string Biography { get; set; }
        public DateTime? DateBirth { get; set; }
        public DateTime? DateDeath { get; set; }

        public ICollection<Book> Books { get; } = new List<Book>();
    }
}