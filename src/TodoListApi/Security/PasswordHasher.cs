using System.Security.Cryptography;
using System.Text;

namespace TodoListApi.Security
{
    public class PasswordHasher
    {
        public static (string Hash, string Salt) HashPassword(string password)
        {
            var saltBytes = RandomNumberGenerator.GetBytes(16); // 16 bytes = 128 bits
            var salt = Convert.ToBase64String(saltBytes);

            var hash = ComputeHash(password, salt);
            return (hash, salt);
        }

        public static bool VerifyPassword(string password, string hashedPassword, string salt)
        {
            var computedHash = ComputeHash(password, salt);
            return computedHash == hashedPassword;
        }
        private static string ComputeHash(string password, string salt)
        {
            var combined = password + salt;
            var bytes = Encoding.UTF8.GetBytes(combined);

            using var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(bytes);

            var sb = new StringBuilder();
            foreach (var b in hashBytes)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }
    }
}
