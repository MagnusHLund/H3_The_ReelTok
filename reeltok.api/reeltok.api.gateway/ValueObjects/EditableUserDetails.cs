namespace reeltok.api.gateway.ValueObjects
{
    public class EditableUserDetails
    {
        public string Username { get; }
        public string Email { get; }

        public EditableUserDetails(string username, string email)
        {
            Username = username;
            Email = email;
        }
    }
}
