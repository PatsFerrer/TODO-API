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
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
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

            _logger.LogInformation("[AUTH] Login attempt Username: {Username}", dto.Username);
            return Ok(new LoginResponseDTO { Token = token });
        }
    }
}
