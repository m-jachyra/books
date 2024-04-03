using AutoMapper;
using Backend.Models.Base;

namespace Backend.Models.Review
{
    public class ReviewDetailsDto : IDetailsDto, IMapFrom
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string IsPositive { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Data.Entities.Review, ReviewDetailsDto>();
        }
    }
}