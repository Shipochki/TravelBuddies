namespace TravelBuddies.Application.Common.Exceptions
{
    public class UnableToCreateApplicationUserException : BaseException
    {
        public UnableToCreateApplicationUserException(string message) : base(message)
        {
        }

        public UnableToCreateApplicationUserException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
