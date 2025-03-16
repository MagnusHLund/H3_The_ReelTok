using reeltok.api.auth.Interfaces.Entities;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.auth.ValueObjects
{
    public class AccessToken : IToken
    {
        [Required]
        public string TokenValue { get; private set; }

        [Required]
        public long CreatedAt { get; private set; }

        [Required]
        public long ExpiresAt { get; private set; }

        public AccessToken(string tokenValue, long createdAt, long expiresAt)
        {
            TokenValue = tokenValue;
            CreatedAt = createdAt;
            ExpiresAt = expiresAt;
        }
    }
}
