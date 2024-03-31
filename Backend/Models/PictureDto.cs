using AutoMapper;
using Backend.Data.Entities;
using Backend.Models.Base;

namespace Backend.Models
{
    public class PictureDto : IMapFrom
    {
        public int Id { get; set; }
        public IFormFile File { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<PictureDto, Book>()
                .ForMember(d => d.PicturePath, s => s.MapFrom(x => $"picture/author_{Id}"));
            
            profile.CreateMap<PictureDto, Author>()
                .ForMember(d => d.PicturePath, s => s.MapFrom(x => $"picture/author_{Id}"));
        }
    }
}