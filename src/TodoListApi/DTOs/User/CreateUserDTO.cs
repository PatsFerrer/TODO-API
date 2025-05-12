using System.ComponentModel.DataAnnotations;

namespace TodoListApi.DTOs.User
{
    public class CreateUserDTO
    {
        [Required]
        [MinLength(4)]
        public string Username { get; set; }

        [MinLength(8)]
        [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z\d]).+$",
            ErrorMessage = "A senha deve conter ao menos uma letra maiúscula, uma letra minúscula, um número e um caractere especial.")]
        public string Password { get; set; }
    }
}
