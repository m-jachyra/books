using System.Text.Json.Serialization;
using AutoMapper;
using AutoMapper.Internal;
using Backend.Data.Entities;
using Backend.Models.Base;

namespace Backend.Models
{
    public class GenreDto : EntityDto, IMapFrom
    {
        public required string Name { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Genre, GenreDto>().ReverseMap();
        }
    }
}