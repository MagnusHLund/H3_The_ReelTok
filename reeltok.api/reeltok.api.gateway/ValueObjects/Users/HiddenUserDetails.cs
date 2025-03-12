namespace reeltok.api.gateway.ValueObjects
{
    public class HiddenUserDetails
    {
        public string Email { get; }

        public HiddenUserDetails(string email)
        {
            Email = email;
        }
    }
}
