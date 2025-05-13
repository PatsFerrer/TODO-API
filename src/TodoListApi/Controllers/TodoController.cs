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

            return CreatedAtAction(nameof(Create), new { id = createdTodo.Id }, response);

        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            var todos = await _service.GetTodosByUserAsync(User);

            var response = new ApiResponse<IEnumerable<TodoResponseDTO>>(
                "Todos fetched successfully",
                todos
            );

            return Ok(response);
        }

        [Authorize]
        [HttpPatch("status/{id}")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateTodoStatusDTO dto)
        {
            var userId = GetUserIdFromToken();
            var updated = await _service.UpdateStatusAsync(id, userId, dto.IsCompleted);

            if (!updated)
                return NotFound(new { message = "Todo não encontrado ou não pertence a você" });

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = GetUserIdFromToken();
            var deleted = await _service.DeleteTodoAsync(id, userId);

            if (!deleted)
                return NotFound(new { message = "Todo não encontrado ou não pertence a você" });

            return NoContent();
        }

        private Guid GetUserIdFromToken()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier);
            return Guid.Parse(userIdClaim!.Value);
        }
    }
}
