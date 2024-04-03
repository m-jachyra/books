using AutoMapper;
using Backend.Models.Base;

namespace Backend.Models.Book
{
    public class BookDetailsDto : IDetailsDto, IMapFrom
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PicturePath { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int GenreId { get; set; }
        public string GenreName { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Data.Entities.Book, BookDetailsDto>()
                .ForMember(d => d.AuthorName, o => o.MapFrom(s => s.Author.Name))
                .ForMember(d => d.GenreName, o => o.MapFrom(s => s.Genre.Name));
        }
    }
}