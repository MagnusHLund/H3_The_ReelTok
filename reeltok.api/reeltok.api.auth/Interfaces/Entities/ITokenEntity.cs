using System.ComponentModel.DataAnnotations;

namespace reeltok.api.auth.Interfaces.Entities
{
    public interface ITokenEntity<TToken> where TToken : IToken
    {
        [Key]
        uint TokenId { get; set; }
        TToken Token { get; set; }
        long? RevokedAt { get; set; }
    }
}
