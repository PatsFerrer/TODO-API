using Microsoft.AspNetCore.Mvc;
using TodoListApi.DTOs.User;
using TodoListApi.Services.Interface;

namespace TodoListApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService service, ILogger<UserController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDTO dto)
        {
            var user = await _service.CreateUserAsync(dto);
            _logger.LogInformation("[USER CREATED] Username: {Username}", dto.Username);

            return CreatedAtAction(nameof(Create), new { id = user.Id }, user);
        }
    }
}
