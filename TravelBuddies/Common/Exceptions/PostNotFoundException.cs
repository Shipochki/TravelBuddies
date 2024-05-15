namespace TravelBuddies.Application.Common.Exceptions
{
    public class PostNotFoundException : BaseException
    {
        public PostNotFoundException(string message)
            : base(message)
        {
        }

        public PostNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
