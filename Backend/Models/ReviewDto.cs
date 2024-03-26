using AutoMapper;
using Backend.Data.Entities;
using Backend.Models.Base;

namespace Backend.Models
{
    public class ReviewDto : EntityDto, IMapFrom
    {
        public bool IsPositive { get; set; }
        public string Content { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Review, ReviewDto>().ReverseMap();
        }
    }
}