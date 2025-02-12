using System.Text;
using System.Security.Cryptography;
using reeltok.api.auth.ValueObjects;
using System.Text.RegularExpressions;

namespace reeltok.api.auth.Utils
{
    internal static class PasswordUtils
    {
        private const int SaltSize = 16; // 16 bytes = 128 bits
        private const int KeySize = 32;  // 32 bytes = 256 bits
        private const int Iterations = 100000;

        private static byte[] GenerateSalt()
        {
            byte[] saltBytes = new byte[SaltSize];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }

            return saltBytes;
        }

        internal static HashedPasswordData HashPassword(string password)
        {
            byte[] saltBytes = GenerateSalt();

            byte[] hashedBytes = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                saltBytes,
                Iterations,
                HashAlgorithmName.SHA256,
                KeySize
            );

            string hashedPassword = Convert.ToBase64String(hashedBytes);
            string salt = Convert.ToBase64String(saltBytes);

            return new HashedPasswordData(hashedPassword, salt);
        }

        internal static bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            byte[] saltBytes = Convert.FromBase64String(storedSalt);

            byte[] hashedBytes = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                saltBytes,
                Iterations,
                HashAlgorithmName.SHA256,
                KeySize
            );

            return Convert.ToBase64String(hashedBytes) == storedHash;
        }

    /// <summary>
    /// This method ensures that the passwords follow the minimum password requirements.
    /// 1. Ensures length is minimum 8 characters long.
    /// 2. Ensures that the password contains at least 1 number.
    /// 3. Ensures that the password contains at least 1 uppercase character.
    /// 4. Ensures that the password contains at least 1 lowercase character.
    /// 5. Ensures that the password does not repeat the same character 3 or more times in a row.
    /// </summary>
    /// <param name="password">Plain text password, which gets validated, to ensure that the minimum password requirements are being followed</param>
    /// <returns></returns>
        internal static bool IsValid(string password)
        {
            return password.Length >= 8 &&
            Regex.IsMatch(password, @"\d") &&
            Regex.IsMatch(password, @"[A-Z]") &&
            Regex.IsMatch(password, @"[a-z]") &&
            !Regex.IsMatch(password, @"(.)\1{2,}");
        }
    }
}
