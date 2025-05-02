namespace TodoListApi.DTOs.Todo
{
    public class TodoResponseDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
