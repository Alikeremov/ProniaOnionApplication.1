using ProniaOnionAPİ.Application.DTOs.CategoryDtos;
using ProniaOnionAPİ.Application.DTOs.ColorDtos;
using ProniaOnionAPİ.Application.DTOs.TagDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnionAPİ.Application.Abstractions.Services
{
    public interface IColorService
    {
        Task<ICollection<ColorItemDto>> GetAllAsync(int page, int take);
        Task<ColorItemDto> GetAsync(int id);
        Task Create(ColorCreateDto categoryDto);
        Task Update(ColorUpdateDto categoryDto, int id);
        Task SoftDeleteAsync(int id);
        Task ReverseDelete(int id);
        Task Delete(int id);
    }
}
