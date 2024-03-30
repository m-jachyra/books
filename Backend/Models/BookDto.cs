using AutoMapper;
using Backend.Data.Entities;
using Backend.Models.Base;

namespace Backend.Models
{
    public class BookDto : EntityDto, IMapFrom
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Book, BookDto>().ReverseMap();
        }
    }
}