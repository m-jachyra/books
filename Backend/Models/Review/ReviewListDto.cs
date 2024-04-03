using AutoMapper;
using Backend.Models.Base;

namespace Backend.Models.Review
{
    public class ReviewListDto : IListDto, IMapFrom
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Preview { get; set; }
        public string IsPositive { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Data.Entities.Review, ReviewListDto>()
                .ForMember(d => d.Preview, o => o.MapFrom(s => s.Content.Take(20)));
        }
    }
}