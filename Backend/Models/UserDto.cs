using AutoMapper;
using Backend.Data.Entities;
using Backend.Models.Base;

namespace Backend.Models
{
    public class UserDto : EntityDto, IMapFrom
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDto>().ReverseMap();
        }
    }
}