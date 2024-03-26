using AutoMapper;
using Backend.Data.Entities;
using Backend.Models.Base;

namespace Backend.Models
{
    public class BookDto : EntityDto, IMapFrom
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Book, BookDto>().ReverseMap();
        }
    }
}