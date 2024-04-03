using AutoMapper;
using Backend.Models.Base;

namespace Backend.Models.Author
{
    public class AuthorUpdateDto : IUpdateDto, IMapFrom
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string? Biography { get; set; }
        public DateTime? DateBirth { get; set; }
        public DateTime? DateDeath { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<AuthorUpdateDto, Data.Entities.Author>();
        }
    }
}