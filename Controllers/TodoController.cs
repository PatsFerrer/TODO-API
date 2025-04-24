using Microsoft.AspNetCore.Mvc;
using TodoListApi.DTOs.Todo;
using TodoListApi.Services;
using TodoListApi.Services.Interface;

namespace TodoListApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _service;

        public TodoController(ITodoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTodoDTO dto)
        {
            await _service.CreateTodoAsync(dto);
            return Ok(new { message = "Todo created successfully" });
        }
    }
}
