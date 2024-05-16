namespace TravelBuddies.Application.Common.Exceptions.NotFound
{
    public class ReviewNotFoundException : NotFoundBaseException
    {
        public ReviewNotFoundException(string message) : base(message)
        {
        }

        public ReviewNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
