namespace TodoListApi.Exceptions
{
    public class UsernameAlreadyExistsException : Exception
    {
        public UsernameAlreadyExistsException()
            : base("Este nome de usuário já está em uso.") { }
    }
}
