using reeltok.api.auth.Interfaces.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.auth.ValueObjects
{
    public class RefreshToken : IToken
    {
        [Required]
        public string TokenValue { get; private set; }

        [Required]
        public long CreatedAt { get; private set; }

        [Required]
        public long ExpiresAt { get; private set; }

        public RefreshToken(string tokenValue, long createdAt, long expiresAt)
        {
            TokenValue = tokenValue;
            CreatedAt = createdAt;
            ExpiresAt = expiresAt;
        }
    }
}
