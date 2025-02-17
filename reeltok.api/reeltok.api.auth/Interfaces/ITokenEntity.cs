using System.ComponentModel.DataAnnotations;

namespace reeltok.api.auth.Interfaces
{
    public interface ITokenEntity<TToken> where TToken : IToken
    {
        [Key]
        uint TokenId { get; set; }
        TToken Token { get; set; }
        uint? RevokedAt { get; set; }
    }
}
