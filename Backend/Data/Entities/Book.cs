using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace Backend.Data.Entities
{
    public class Book : IHasId, ISortable<Book>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        
        public static Expression<Func<Book, object>> GetSortProperty(string? sortColumn) =>
            sortColumn?.ToLower() switch
            {
                "title" => book => book.Title,
                "genre" => book => book.Genre.Name,
                "author" => book => book.Author.Name,
                _ => book => book.Id
            };
    }
}