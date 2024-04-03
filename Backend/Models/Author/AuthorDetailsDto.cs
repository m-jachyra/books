using AutoMapper;
using Backend.Models.Base;
using Backend.Data.Entities;

namespace Backend.Models.Author
{
    public class AuthorDetailsDto : IDetailsDto, IMapFrom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public string PicturePath { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Data.Entities.Author, AuthorDetailsDto>();
        }
    }
}