using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProniaOnionAPİ.Application.Abstractions.Repositories;
using ProniaOnionAPİ.Application.Abstractions.Services;
using ProniaOnionAPİ.Application.DTOs.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnionAPİ.Persistence.Implementations.Services
{
    public class ProductService:IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductItemDto>> GetAllAsync(int page,int take)
        {
            return _mapper.Map<IEnumerable<ProductItemDto>>(await _repository.GetAllWhere(skip: (page - 1) * 3, take: take).ToListAsync());
        }
    }
}
