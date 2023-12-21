using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaOnionAPİ.Application.Abstractions.Services;
using ProniaOnionAPİ.Application.DTOs.TagDtos;

namespace ProniaOnionAPİ.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _service;

        public TagsController(ITagService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int page=1,int take=3)
        {
            return Ok(await _service.GetAllAsync(page, take));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]TagCreateDto dto)
        {
            await _service.Create(dto);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,[FromForm]TagUpdateDto dto)
        {
            await _service.Update(dto,id);
            return Ok();
        }
    }
}
