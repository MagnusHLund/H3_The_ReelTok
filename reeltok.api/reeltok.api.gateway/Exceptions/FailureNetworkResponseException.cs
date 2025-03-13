namespace reeltok.api.gateway.Exceptions
{
    // This exception exists, to let the user know a network request failed, with the message from the other microservice, which it was thrown in.
    public class FailureNetworkResponseException : Exception
    {
        public FailureNetworkResponseException() { }
        public FailureNetworkResponseException(string message) : base(message) { }
        public FailureNetworkResponseException(string message, Exception innerException) : base(message, innerException) { }
    }
}