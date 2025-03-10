using reeltok.api.auth.Interfaces.Entities;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.auth.ValueObjects
{
    public class AccessToken : IToken
    {
        [Required]
        public string TokenValue { get; private set; }

        [Required]
        public uint CreatedAt { get; private set; }

        [Required]
        public uint ExpiresAt { get; private set; }

        public AccessToken(string tokenValue, uint createdAt, uint expiresAt)
        {
            TokenValue = tokenValue;
            CreatedAt = createdAt;
            ExpiresAt = expiresAt;
        }
    }
}
