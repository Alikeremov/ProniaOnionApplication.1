using ProniaOnionAPİ.Application.DTOs.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnionAPİ.Application.Abstractions.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductItemDto>> GetAllAsync(int page, int take);
    }
}
