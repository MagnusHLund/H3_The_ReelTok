using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text;

public static class PasswordUtils
{
    private const int SaltSize = 16; // 16 bytes = 128 bits
    private const int KeySize = 32;  // 32 bytes = 256 bits
    private const int Iterations = 100000;

    public static (string hashedPassword, string salt) HashPassword(string password)
    {
        var saltBytes = new byte[SaltSize];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(saltBytes);
        }

        var hashedBytes = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            saltBytes,
            Iterations,
            HashAlgorithmName.SHA256,
            KeySize
        );

        return (Convert.ToBase64String(hashedBytes), Convert.ToBase64String(saltBytes));
    }

    public static bool VerifyPassword(string password, string storedHash, string storedSalt)
    {
        var saltBytes = Convert.FromBase64String(storedSalt);

        var hashedBytes = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            saltBytes,
            Iterations,
            HashAlgorithmName.SHA256,
            KeySize
        );

        return Convert.ToBase64String(hashedBytes) == storedHash;
    }

    public static bool IsValid(string password)
    {
       return password.Length >= 8 &&
       Regex.IsMatch(password, @"\d") &&
       Regex.IsMatch(password, @"[A-Z]") &&
       Regex.IsMatch(password, @"[a-z]") &&
       !Regex.IsMatch(password, @"(.)\1{2,}"); 
    }
}

