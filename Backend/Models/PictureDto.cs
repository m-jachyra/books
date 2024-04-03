using AutoMapper;
using Backend.Data.Entities;
using Backend.Models.Base;

namespace Backend.Models
{
    public class PictureDto : IMapFrom
    {
        public required int Id { get; set; }
        public required IFormFile File { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<PictureDto, Data.Entities.Book>()
                .ForMember(d => d.PicturePath, s => s.MapFrom(x => $"picture/author_{Id}"));
            
            profile.CreateMap<PictureDto, Data.Entities.Author>()
                .ForMember(d => d.PicturePath, s => s.MapFrom(x => $"picture/author_{Id}"));
        }
    }
}