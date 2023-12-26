using AutoMapper;
using ProniaOnionAPİ.Application.DTOs.ProductDtos;
using ProniaOnionAPİ.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnionAPİ.Application.MappingProfiles
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductItemDto,Product>().ReverseMap();
            CreateMap<Product,ProductGetDto>().ReverseMap();
            CreateMap<Product,ProductUpdateDto>().ReverseMap();
            CreateMap<ProductCreateDto,Product>().ReverseMap();
        }
    }
}
