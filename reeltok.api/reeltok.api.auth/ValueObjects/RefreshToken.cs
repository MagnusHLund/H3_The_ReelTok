using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using reeltok.api.auth.Interfaces;

namespace reeltok.api.auth.ValueObjects
{
    [Index(nameof(TokenValue), IsUnique = true, Name = "UX_AccessToken_TokenValue")]
    [Index(nameof(ExpiresAt), Name = "IX_AccessToken_ExpiresAt")]
    [Index(nameof(UserId), Name = "IX_AccessToken_UserId")]
    public class RefreshToken : IToken
    {
        [Required]
        public Guid UserId { get; private set; }

        [Required]
        public string TokenValue { get; private set; }

        [Required]
        public uint CreatedAt { get; private set; }

        [Required]
        public uint ExpiresAt { get; private set; }

        public RefreshToken(Guid userId, string tokenValue, uint createdAt, uint expiresAt)
        {
            UserId = userId;
            TokenValue = tokenValue;
            CreatedAt = createdAt;
            ExpiresAt = expiresAt;
        }
    }
}
