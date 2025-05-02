namespace TodoListApi.Exceptions
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string? ErrorCode { get; set; }
    }
}
