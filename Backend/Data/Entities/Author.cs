using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Backend.Data.Entities
{
    public class Author : IHasId, ISortable<Author>
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        public string Biography { get; set; }
        public DateTime? DateBirth { get; set; }
        public DateTime? DateDeath { get; set; }

        public ICollection<Book> Books { get; } = new List<Book>();
        public static Expression<Func<Author, object>> GetSortProperty(string? sortColumn)=>
            sortColumn?.ToLower() switch
            {
                "Name" => author => author.Name,
                _ => author => author.Id
            };
    }
}