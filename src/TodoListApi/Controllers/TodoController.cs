using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoListApi.DTOs.Shared;
using TodoListApi.DTOs.Todo;
using TodoListApi.Services.Interface;

namespace TodoListApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _service;
        private readonly ILogger<TodoController> _logger;

        public TodoController(ITodoService service, ILogger<TodoController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTodoDTO dto)
        {
            var createdTodo = await _service.CreateTodoAsync(dto, User);
            _logger.LogInformation("[TODO CREATED] Title: {Title}", dto.Title);

            var response = new ApiResponse<TodoResponseDTO>(
                "Todo created successfully",
                createdTodo
            );

            return Ok(response);
        }
    }
}
