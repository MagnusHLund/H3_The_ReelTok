namespace reeltok.api.auth.Interfaces
{
    public interface IToken
    {
        string TokenValue { get; }

        uint ExpiresAt { get; }
    }
}
