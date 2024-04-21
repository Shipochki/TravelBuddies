namespace TravelBuddies.Application.Exceptions
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
