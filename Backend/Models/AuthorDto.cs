using Backend.Data.Entities;

namespace Backend.Models
{
    public class AuthorDtoBase
    {
        public string Name { get; set; }
        public string Biography { get; set; }
        public DateTime? DateBirth { get; set; }
        public DateTime? DateDeath { get; set; }
    }

    public class AuthorDto : AuthorDtoBase
    {
        public int Id { get; set; }
    }
}