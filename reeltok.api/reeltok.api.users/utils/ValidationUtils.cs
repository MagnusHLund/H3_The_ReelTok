using System.Net.Mail;
using System.Text.RegularExpressions;

namespace reeltok.api.users.utils
{
    internal static class ValidationUtils
    {
        internal static bool IsValidEmail(string email)
        {
            Regex emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);

            return emailRegex.IsMatch(email);
        }

        internal static bool IsValidUsername(string username)
        {
            return username.Length > 3 && username.Length < 25;
        }
    }
}