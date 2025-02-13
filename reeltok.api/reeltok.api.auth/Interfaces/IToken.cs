namespace reeltok.api.auth.Interfaces
{
    public interface IToken
    {
        string Token { get; }

        DateTime CreateDate { get; }

        DateTime ExpireDate { get; }
    }
}
