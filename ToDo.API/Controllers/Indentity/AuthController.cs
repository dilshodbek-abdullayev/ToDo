using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.Application.Abstractions.IServices;
using ToDo.Domain.Entities.DTOs;

namespace ToDo.API.Controllers.Indentity
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] RequestLogin loginRequest)
        {
            var res = await _authService.GenerateToken(loginRequest);

            return Ok(res);
        }
    }
}
