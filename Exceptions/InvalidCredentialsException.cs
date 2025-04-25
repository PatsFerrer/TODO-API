namespace TodoListApi.Exceptions
{
    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException()
            : base("As credenciais fornecidas são inválidas.") { }
    }
}
