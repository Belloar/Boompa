namespace Boompa.Exceptions
{
    public class CloudException : Exception
    {
        public CloudException()
        {
        }

        public CloudException(string? message) : base(message)
        {
        }
    }
}
