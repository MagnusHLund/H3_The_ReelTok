namespace reeltok.api.auth.Interfaces.Entities
{
    public interface IToken
    {
        string TokenValue { get; }

        uint ExpiresAt { get; }
    }
}
