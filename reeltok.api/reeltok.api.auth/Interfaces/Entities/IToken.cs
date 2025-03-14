namespace reeltok.api.auth.Interfaces.Entities
{
    public interface IToken
    {
        string TokenValue { get; }

        long ExpiresAt { get; }
    }
}
