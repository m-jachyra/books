﻿using AutoMapper;
using Backend.Data.Entities;
using Backend.Models.Base;

namespace Backend.Models
{
    public class AuthorDto : EntityDto, IMapFrom
    {
        public string Name { get; set; }
        public string Biography { get; set; }
        public DateTime? DateBirth { get; set; }
        public DateTime? DateDeath { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Author, AuthorDto>().ReverseMap();
        }
    }
}