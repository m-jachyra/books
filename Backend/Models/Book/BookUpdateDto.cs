using AutoMapper;
using Backend.Models.Base;

namespace Backend.Models.Book
{
    public class BookUpdateDto : IUpdateDto, IMapFrom
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required int GenreId { get; set; }
        public required int AuthorId { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<BookUpdateDto, Data.Entities.Book>();
        }
    }
}