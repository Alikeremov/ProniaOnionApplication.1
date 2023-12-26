using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnionAPİ.Application.DTOs.ProductDtos
{
    public record ProductCreateDto(string Name,decimal Price,string? Description,string SKU,int CategoryId,ICollection<int> ColorIds,ICollection<int> TagIds);
}
