using reeltok.api.auth.Interfaces;
using reeltok.api.auth.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.auth.Entities
{
    public class RefreshTokenEntity : ITokenEntity<RefreshToken>
    {
        [Key]
        public uint TokenId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public RefreshToken Token { get; set; }

        public uint? RevokedAt { get; set; }

        public RefreshTokenEntity(uint tokenId, Guid userId, RefreshToken refreshToken, uint? revokedAt)
        {
            TokenId = tokenId;
            UserId = userId;
            Token = refreshToken;
            RevokedAt = revokedAt;
        }

        public RefreshTokenEntity() { }
    }
}
