﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaOnionAPİ.Application.Abstractions.Repositories;
using ProniaOnionAPİ.Application.Abstractions.Services;
using ProniaOnionAPİ.Application.DTOs.TagDtos;
using ProniaOnionAPİ.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnionAPİ.Persistence.Implementations.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _repository;
        private readonly IMapper _mapper;

        public TagService(ITagRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ICollection<TagItemDto>> GetAllAsync(int page, int take)
        {
            ICollection<Tag> tags = await _repository.GetAllWhere(skip: (page - 1) * take, take: take).ToListAsync();
            return _mapper.Map<ICollection<TagItemDto>>(tags);
        }
        public async Task<TagItemDto> GetAsync(int id)
        {
            Tag tag = await _repository.GetByIdAsync(id);
            if (tag == null) throw new Exception("Not Found");

            return _mapper.Map<TagItemDto>(tag);
        }
        public async Task Create(TagCreateDto tagDto)
        {
            if (await _repository.Cheeck(x => x.Name == tagDto.Name)) throw new Exception("Bad Request");
            await _repository.AddAsync(_mapper.Map<Tag>(tagDto));
            await _repository.SaveChangesAsync();
        }


        public async Task Update(TagUpdateDto tagDto, int id)
        {
            Tag existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new Exception("Not Found");
            if (await _repository.Cheeck(x => x.Name == tagDto.Name)) throw new Exception("Bad Request");
            
            _repository.Update(_mapper.Map(tagDto,existed));
            await _repository.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            Tag tag=await _repository.GetByIdAsync(id,true);
            if (tag == null) throw new Exception("Not Found");
            _repository.SoftDelete(tag);
            await _repository.SaveChangesAsync();
        }
        public async Task ReverseDelete(int id)
        {
            Tag tag = await _repository.GetByIdAsync(id, true,ignoreQuery: true);
            if (tag == null) throw new Exception("Not Found");
            _repository.ReverseDelete(tag);
            await _repository.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            Tag tag = await _repository.GetByIdAsync(id);
            if (tag == null) throw new Exception("Not Found");
            _repository.Delete(tag);
            await _repository.SaveChangesAsync();
        }
        
        
    }
}
