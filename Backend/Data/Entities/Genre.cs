using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Backend.Data.Entities
{
    public class Genre : IHasId, ISortable<Genre>
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }

        public ICollection<Book> Books { get; } = new List<Book>();

        public static Expression<Func<Genre, object>> GetSortProperty(string? sortColumn) =>
            sortColumn?.ToLower() switch
            {
                "name" => genre => genre.Name,
                _ => genre => genre.Id
            };
    }
}