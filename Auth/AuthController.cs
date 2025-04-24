using Microsoft.AspNetCore.Mvc;
using TodoListApi.Auth.Interface;
using TodoListApi.DTOs.Auth;

namespace TodoListApi.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Username) || string.IsNullOrEmpty(dto.Password))
            {
                return BadRequest("Invalid login request");
            }
            var token = await _authService.LoginAsync(dto);
            if (token == null)
            {
                return Unauthorized("Invalid username or password");
            }
            return Ok(new LoginResponseDTO { Token = token });
        }
    }
}
