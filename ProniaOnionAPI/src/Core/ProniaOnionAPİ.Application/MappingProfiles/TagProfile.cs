using AutoMapper;
using ProniaOnionAPİ.Application.DTOs.CategoryDtos;
using ProniaOnionAPİ.Application.DTOs.TagDtos;
using ProniaOnionAPİ.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnionAPİ.Application.MappingProfiles
{
    public class TagProfile:Profile
    {
        public TagProfile()
        {
            CreateMap<Tag, TagItemDto>().ReverseMap();
            CreateMap<TagCreateDto,Tag>();
            CreateMap<Tag, TagUpdateDto>().ReverseMap();

        }
    }
}
