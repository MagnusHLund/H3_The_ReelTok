using System.ComponentModel.DataAnnotations;
using reeltok.api.auth.ValueObjects;

namespace reeltok.api.auth.Entities
{
    public class AccessTokenEntity
    {
        [Key]
        public uint TokenId { get; set; }

        [Required]
        public AccessToken AccessToken { get; set; }

        public uint RevokedAt { get; set; }

        public AccessTokenEntity(uint tokenId, AccessToken accessToken, uint revokedAt)
        {
            TokenId = tokenId;
            AccessToken = accessToken;
            RevokedAt = revokedAt;
        }
    }
}
