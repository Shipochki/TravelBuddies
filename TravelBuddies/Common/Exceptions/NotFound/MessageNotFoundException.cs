namespace TravelBuddies.Application.Common.Exceptions.NotFound
{
    public class MessageNotFoundException : NotFoundBaseException
    {
        public MessageNotFoundException(string message)
            : base(message)
        {
        }

        public MessageNotFoundException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
