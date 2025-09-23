namespace Boompa.Exceptions
{
    public class RepoException : Exception
    {
        public RepoException()
        {
        }
        public RepoException(string message) : base(message)
        {
        }
    }
}
