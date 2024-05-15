namespace TravelBuddies.Application.Common.Exceptions
{
    public class MessageNotFoundException : BaseException
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
