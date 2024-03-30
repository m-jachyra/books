using AutoMapper;
using Backend.Data.Entities;
using Backend.Models.Base;

namespace Backend.Models
{
    public class ReviewDto : EntityDto, IMapFrom
    {
        public required bool IsPositive { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required int BookId { get; set; }
        public required int PlusCount { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Review, ReviewDto>()
                .ForMember(d => d.PlusCount, s => s.MapFrom(x => x.UserReviewPluses.Count))
                .ReverseMap();
        }
    }
}