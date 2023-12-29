using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaOnionAPİ.Application.Abstractions.Services;
using ProniaOnionAPİ.Application.DTOs.User;

namespace ProniaOnionAPİ.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AppUsersController : ControllerBase
    {
        private readonly IAutenticationService _service;

        public AppUsersController(IAutenticationService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromForm]RegisterDto dto)
        {
            await _service.Register(dto);
            return StatusCode(StatusCodes.Status204NoContent);
        }
        [HttpPost("[Action]")]
        public async Task<IActionResult> Login([FromForm]LoginDto dto)
        {
            return StatusCode(StatusCodes.Status200OK, await _service.Login(dto));
            
        }
    }
}
