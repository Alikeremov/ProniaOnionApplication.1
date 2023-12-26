using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaOnionAPİ.Application.Abstractions.Services;
using ProniaOnionAPİ.Application.DTOs.CategoryDtos;

namespace ProniaOnionAPİ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int page=1,int take=3)
        {
           
            return Ok(await _service.GetAllAsync(page, take));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            return StatusCode(StatusCodes.Status200OK,await _service.GetAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CategoryCreateDto categorydto)
        {
            await _service.Create(categorydto);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] CategoryUpdateDto categoryDto)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            await _service.Update(categoryDto, id);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            await _service.Delete(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);

            await _service.SoftDeleteAsync(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
        [HttpPatch("recovery/{id}")]
        public async Task<IActionResult> Recovery(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);

            await _service.ReverseDelete(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
        
    }
}
