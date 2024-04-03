using AutoMapper;
using Backend.Models.Base;

namespace Backend.Models.Review
{
    public class ReviewUpdateDto : IUpdateDto, IMapFrom
    {
        public required int Id { get; set; }
        public required int BookId { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required bool IsPositive { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ReviewUpdateDto, Data.Entities.Review>();
        }
    }
}