using AutoMapper;
using Backend.Models.Base;

namespace Backend.Models.Review
{
    public class ReviewListDto : IListDto, IMapFrom
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Preview { get; set; }
        public bool IsPositive { get; set; }
        public string PlusCount { get; set; }
        public bool? IsPlussed { get; set; }
        public string UserName { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Data.Entities.Review, ReviewListDto>()
                .ForMember(d => d.Preview, o => o.MapFrom(s => s.Content.Take(20)))
                .ForMember(d => d.UserName, o => o.MapFrom(s => s.User.UserName))
                .ForMember(d => d.PlusCount, o => o.MapFrom(s => s.UserReviewPluses.Count))
                .ForMember(d => d.IsPlussed, o => o.Ignore());
        }
    }
}