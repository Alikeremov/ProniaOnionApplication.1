using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaOnionAPİ.Application.Abstractions.Repositories;
using ProniaOnionAPİ.Application.Abstractions.Services;
using ProniaOnionAPİ.Application.DTOs.ColorDtos;
using ProniaOnionAPİ.Application.DTOs.TagDtos;
using ProniaOnionAPİ.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnionAPİ.Persistence.Implementations.Services
{
    public class ColorService : IColorService
    {
        private readonly IColorRepository _repository;
        private readonly IMapper _mapper;

        public ColorService(IColorRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ICollection<ColorItemDto>> GetAllAsync(int page, int take)
        {
            ICollection<Color> colors = await _repository.GetAllWhere(skip: (page - 1) * take, take: take).ToListAsync();
            return _mapper.Map<ICollection<ColorItemDto>>(colors);
        }
        public async Task<ColorItemDto> GetAsync(int id)
        {
            Color color = await _repository.GetByIdAsync(id);
            if (color == null) throw new Exception("Not Found");

            return _mapper.Map<ColorItemDto>(color);
        }

        public async Task Create(ColorCreateDto colordto)
        {
            if (await _repository.Cheeck(x => x.Name == colordto.Name)) throw new Exception("Bad Request");
            await _repository.AddAsync(_mapper.Map<Color>(colordto));
            await _repository.SaveChangesAsync();
        }


        public async Task Update(ColorUpdateDto colordto, int id)
        {
            Color existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new Exception("Not Found");
            if (await _repository.Cheeck(x => x.Name == colordto.Name)) throw new Exception("Bad Request");

            _repository.Update(_mapper.Map(colordto, existed));
            await _repository.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            Color color = await _repository.GetByIdAsync(id, true);
            if (color == null) throw new Exception("Not Found");
            _repository.SoftDelete(color);
            await _repository.SaveChangesAsync();
        }
        public async Task ReverseDelete(int id)
        {
            Color color = await _repository.GetByIdAsync(id, true,ignoreQuery: true);
            if (color == null) throw new Exception("Not Found");
            _repository.ReverseDelete(color);
            await _repository.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            Color color = await _repository.GetByIdAsync(id);
            if (color == null) throw new Exception("Not Found");
            _repository.Delete(color);
            await _repository.SaveChangesAsync();
        }
    }
}
