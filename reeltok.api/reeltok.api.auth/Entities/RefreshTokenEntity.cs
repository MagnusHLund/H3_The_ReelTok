using System.ComponentModel.DataAnnotations;
using reeltok.api.auth.ValueObjects;

namespace reeltok.api.auth.Entities
{
    public class RefreshTokenEntity
    {
        [Key]
        public uint TokenId { get; set; }

        [Required]
        public RefreshToken RefreshToken { get; set; }

        public uint RevokedAt { get; set; }

        public RefreshTokenEntity(uint tokenId, RefreshToken refreshToken, uint revokedAt)
        {
            TokenId = tokenId;
            RefreshToken = refreshToken;
            RevokedAt = revokedAt;
        }
    }
}
