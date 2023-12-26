using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaOnionAPİ.Application.Abstractions.Services;
using ProniaOnionAPİ.Application.DTOs.TagDtos;

namespace ProniaOnionAPİ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _service;

        public TagsController(ITagService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int take = 3)
        {
            return Ok(await _service.GetAllAsync(page, take));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            return StatusCode(StatusCodes.Status200OK, await _service.GetAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] TagCreateDto dto)
        {
            await _service.Create(dto);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] TagUpdateDto dto)
        {
            await _service.Update(dto, id);
            return Ok();
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
