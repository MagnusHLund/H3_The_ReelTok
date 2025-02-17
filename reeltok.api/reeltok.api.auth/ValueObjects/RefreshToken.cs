using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using reeltok.api.auth.Interfaces;

namespace reeltok.api.auth.ValueObjects
{
    public class RefreshToken : IToken
    {
        [Required]
        public string TokenValue { get; private set; }

        [Required]
        public uint CreatedAt { get; private set; }

        [Required]
        public uint ExpiresAt { get; private set; }

        public RefreshToken(string tokenValue, uint createdAt, uint expiresAt)
        {
            TokenValue = tokenValue;
            CreatedAt = createdAt;
            ExpiresAt = expiresAt;
        }
    }
}
