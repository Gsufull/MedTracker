using System;
using System.Security.Cryptography;
using System.Text;

namespace MedTracker.Helpers
{
    // Утиліта для хешування паролів
    public static class PasswordHelper
    {
        public static string Hash(string password)
        {
            // Використовуємо SHA256 для хешування
            using var sha = SHA256.Create();
            byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            var sb = new StringBuilder();
            foreach (var b in bytes)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }

        public static bool Verify(string password, string hash)
        {
            return Hash(password) == hash;
        }
    }
}
