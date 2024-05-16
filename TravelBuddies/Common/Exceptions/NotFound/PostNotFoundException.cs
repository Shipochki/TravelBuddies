namespace TravelBuddies.Application.Common.Exceptions.NotFound
{
    public class PostNotFoundException : NotFoundBaseException
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
