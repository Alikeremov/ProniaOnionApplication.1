using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaOnionAPİ.Application.Abstractions.Repositories;
using ProniaOnionAPİ.Application.Abstractions.Services;
using ProniaOnionAPİ.Application.DTOs.CategoryDtos;
using ProniaOnionAPİ.Application.DTOs.TagDtos;
using ProniaOnionAPİ.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnionAPİ.Persistence.Implementations.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ICollection<CategoryItemDto>> GetAllAsync(int page, int take)
        {
            ICollection<Category> categories = await _repository.GetAllWhere(skip: (page - 1) * take, take: take).ToListAsync();           
            return _mapper.Map<ICollection<CategoryItemDto>>(categories);
        }
        public async Task<CategoryItemDto> GetAsync(int id)
        {
            Category category = await _repository.GetByIdAsync(id);
            if (category == null) throw new Exception("Not Found");

            return _mapper.Map<CategoryItemDto>(category);
        }
        public async Task Create(CategoryCreateDto dto)
        {
            if (await _repository.Cheeck(x => x.Name == dto.Name)) throw new Exception("Bad Request");        
            await _repository.AddAsync(_mapper.Map<Category>(dto));
            await _repository.SaveChangesAsync();
        }

        public async Task Update(CategoryUpdateDto categoryDto, int id)
        {
            Category existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new Exception("Not Found");
            if (await _repository.Cheeck(x => x.Name == categoryDto.Name)) throw new Exception("Bad Request");
            _repository.Update(_mapper.Map(categoryDto, existed));
            await _repository.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            Category category = await _repository.GetByIdAsync(id,true);
            if (category == null) throw new Exception("Not Found");
            _repository.SoftDelete(category);
            await _repository.SaveChangesAsync();
        }
        public async Task ReverseDelete(int id)
        {
            Category category = await _repository.GetByIdAsync(id,true,ignoreQuery: true);
            if (category == null) throw new Exception("Not Found");
            _repository.ReverseDelete(category);
            await _repository.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            Category category = await _repository.GetByIdAsync(id);
            if (category == null) throw new Exception("Not Found");
            _repository.Delete(category);
            await _repository.SaveChangesAsync();
        }

        //public async Task Delete(int id)
        //{
        //    Category existed = await _repository.GetByIdAsync(id);
        //    if (existed == null) throw new Exception("NotFound");
        //    _repository.Delete(existed);
        //    await _repository.SaveChangesAsync();
        //}
    }
}
