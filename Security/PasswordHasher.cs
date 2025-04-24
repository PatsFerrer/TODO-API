using System.Security.Cryptography;
using System.Text;

namespace TodoListApi.Security
{
    public class PasswordHasher
    {
        /// <summary>
        /// Gera um hash SHA256 a partir de uma senha em texto puro.
        /// </summary>
        /// <param name="password">Senha em texto puro</param>
        /// <returns>Hash da senha em formato hexadecimal</returns>
        public static string HashPassword(string password)
        {
            // Converte a string para bytes
            var bytes = Encoding.UTF8.GetBytes(password);

            // Cria o hash com SHA256 e converte para string hexadecimal
            using var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(bytes);

            var sb = new StringBuilder();
            foreach (var b in hashBytes)
            {
                sb.Append(b.ToString("x2")); // converte cada byte para hexadecimal (ex: "1f", "a3", etc.)
            }

            return sb.ToString();
        }

        /// <summary>
        /// Compara uma senha em texto com o hash salvo.
        /// </summary>
        /// <param name="password">Senha em texto puro</param>
        /// <param name="hashedPassword">Senha já hashada</param>
        /// <returns>True se forem iguais, False caso contrário</returns>
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            var hashedInput = HashPassword(password);
            return hashedInput == hashedPassword;
        }
    }
}
