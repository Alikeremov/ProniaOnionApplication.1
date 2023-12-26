using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProniaOnionAPİ.Application.Abstractions.Repositories;
using ProniaOnionAPİ.Application.Abstractions.Services;
using ProniaOnionAPİ.Application.DTOs.ProductDtos;
using ProniaOnionAPİ.Domain.Entities;
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
        private readonly ICategoryRepository _categoryRepository;
        private readonly IColorRepository _colorRepository;
        private readonly ITagRepository _tagRepository;

        public ProductService(IProductRepository repository, IMapper mapper,ICategoryRepository categoryRepository,IColorRepository colorRepository,ITagRepository tagRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _colorRepository = colorRepository;
            _tagRepository = tagRepository;
        }
        public async Task<IEnumerable<ProductItemDto>> GetAllAsync(int page,int take)
        {
            return _mapper.Map<IEnumerable<ProductItemDto>>(await _repository.GetAllWhere(skip: (page - 1) * 3, take: take).ToListAsync());
        }
        public async Task<ProductGetDto> GetById(int id)
        {
            Product product =await _repository.GetByIdAsync(id,includes:nameof(Product.Category));
            if (product == null) throw new Exception("This Category does't have in your Categories");
            return _mapper.Map<ProductGetDto>(product);
        }
        public async Task CreateAsync(ProductCreateDto dto)
        {
            if (await _repository.Cheeck(x => x.Name == dto.Name) ) throw new Exception("You have this name product please send other name");

            if (!await _categoryRepository.Cheeck(x => x.Id == dto.CategoryId) || dto.CategoryId!=0) throw new Exception("You don't have this category in your categoryies");

            Product product=_mapper.Map<Product>(dto);
            product.ProductColors=new List<ProductColor>();
            foreach(var colorId in dto.ColorIds)
            {
                if (!await _colorRepository.Cheeck(x => x.Id == colorId)) throw new Exception("You don't have this color");
                product.ProductColors.Add(new ProductColor { ColorId=colorId});
            }
            product.TagProducts=new List<TagProduct>();
            foreach(var tagId in dto.TagIds)
            {
                if (!await _tagRepository.Cheeck(x => x.Id == tagId)) throw new Exception("You don't have this tag");
                product.TagProducts.Add(new TagProduct {  TagId=tagId});
            }
            await _repository.AddAsync(product);
            await _repository.SaveChangesAsync();
        }
        public async Task UpdateAsync(int id,ProductUpdateDto dto)
        {

            Product existed = await _repository.GetByIdAsync(id, true, includes:new string[] { nameof(Product.ProductColors), nameof(Product.TagProducts) });
            if(existed.Name!=dto.Name)
                if (await _repository.Cheeck(x => x.Name == dto.Name)) throw new Exception("You have this name product please send other name");
            if (dto.CategoryId != existed.CategoryId) 
            {
                if (!await _categoryRepository.Cheeck(x => x.Id == dto.CategoryId)) throw new Exception("You don't have this category in your categoryies");
            }
            existed = _mapper.Map(dto, existed);
            if (dto.ColorIds != null)
            {
                foreach (var colorId in dto.ColorIds)
                {
                    if (!existed.ProductColors.Any(pc => pc.ColorId == colorId))
                    {
                        if (!await _colorRepository.Cheeck(x => x.Id == colorId)) throw new Exception("You don't have this color");
                        existed.ProductColors.Add(new ProductColor { ColorId = colorId, });
                    }
                }
                existed.ProductColors = existed.ProductColors.Where(x => dto.ColorIds.Any(colid => x.ColorId == colid)).ToList();
            }
            else
                existed.ProductColors = new List<ProductColor>();
            foreach (var tagid in dto.TagIds)
            {
                if (!existed.TagProducts.Any(pc => pc.TagId == tagid))
                {
                    if (!await _tagRepository.Cheeck(x => x.Id == tagid)) throw new Exception("You don't have this tag");
                    existed.TagProducts.Add(new TagProduct { TagId = tagid, });
                }
            }
            
            existed.TagProducts = existed.TagProducts.Where(x => dto.TagIds.Any(tagid => x.TagId == tagid)).ToList();
            
            _repository.Update(existed);
            await _repository.SaveChangesAsync();
        }
        public async Task SoftDelete(int id)
        {
            Product product = await _repository.GetByIdAsync(id, true, includes: new string[] { nameof(Product.ProductColors), nameof(Product.TagProducts) });
            if (product == null) throw new Exception("Not Found");
            product.ProductColors=product.ProductColors.Where(x => x.ProductId == id).ToList();
            if(product.ProductColors!=null)
            {
                foreach (ProductColor color in product.ProductColors)
                {
                    color.IsDeleted = true;
                }
            }
            product.TagProducts=product.TagProducts.Where(x => x.TagId == id).ToList();
            foreach (var tag in product.TagProducts)
            {
                tag.IsDeleted = true;
            }
            _repository.SoftDelete(product);
            await _repository.SaveChangesAsync();
        }
        public async Task ReverseDelete(int id)
        {
            Product product = await _repository.GetByIdAsync(id, true,ignoreQuery:true, includes: new string[] { nameof(Product.ProductColors), nameof(Product.TagProducts) });
            if (product == null) throw new Exception("Not Found");
            product.ProductColors = product.ProductColors.Where(x => x.ProductId == id).ToList();
            if (product.ProductColors != null)
            {
                foreach (ProductColor color in product.ProductColors)
                {
                    color.IsDeleted = false;
                }
            }
            product.TagProducts = product.TagProducts.Where(x => x.TagId == id).ToList();
            foreach (var tag in product.TagProducts)
            {
                tag.IsDeleted = false;
            }
            _repository.ReverseDelete(product);
            await _repository.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            Product product = await _repository.GetByIdAsync(id);
            if (product == null) throw new Exception("Not Found");
            _repository.Delete(product);
            await _repository.SaveChangesAsync();
        }
    }
}
