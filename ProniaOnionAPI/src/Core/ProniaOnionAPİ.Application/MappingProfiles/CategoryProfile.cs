using AutoMapper;
using ProniaOnionAPİ.Application.DTOs.CategoryDtos;
using ProniaOnionAPİ.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnionAPİ.Application.MappingProfiles
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryItemDto>().ReverseMap();
            CreateMap<CategoryCreateDto,Category>();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();
        }
    }
}
