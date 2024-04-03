using AutoMapper;
using Backend.Models.Base;

namespace Backend.Models.Author
{
    public class AuthorListDto : IListDto, IMapFrom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PicturePath { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Data.Entities.Author, AuthorListDto>();
        }
    }
}