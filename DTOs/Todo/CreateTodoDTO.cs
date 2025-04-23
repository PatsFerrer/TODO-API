namespace TodoListApi.DTOs.Todo
{
    public class CreateTodoDTO
    {
        public string Title { get; set; }
        public Guid UserId { get; set; }
    }
}
