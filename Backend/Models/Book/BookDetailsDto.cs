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
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Data.Entities.Book, BookDetailsDto>();
        }
    }
}