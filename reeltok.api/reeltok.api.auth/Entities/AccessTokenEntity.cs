using reeltok.api.auth.ValueObjects;
using reeltok.api.auth.Interfaces.Entities;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.auth.Entities
{
    public class AccessTokenEntity : ITokenEntity<AccessToken>
    {
        [Key]
        public uint TokenId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public AccessToken Token { get; set; }

        public uint? RevokedAt { get; set; }

        public AccessTokenEntity(uint tokenId, Guid userId, AccessToken accessToken, uint? revokedAt)
        {
            TokenId = tokenId;
            UserId = userId;
            Token = accessToken;
            RevokedAt = revokedAt;
        }

        public AccessTokenEntity() { }
    }
}
