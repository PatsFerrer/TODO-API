using System.ComponentModel.DataAnnotations;

namespace TodoListApi.DTOs.User
{
    public class CreateUserDTO
    {
        [Required]
        [MinLength(4)]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
