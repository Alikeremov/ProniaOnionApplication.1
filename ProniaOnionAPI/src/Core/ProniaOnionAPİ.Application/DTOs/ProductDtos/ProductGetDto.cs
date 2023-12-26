using ProniaOnionAPİ.Application.DTOs.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnionAPİ.Application.DTOs.ProductDtos
{
    public record ProductGetDto(int Id,string Name,decimal Price,string SKU,string? Description,int CategoryId, IncludeCategoryDto Category);
}
