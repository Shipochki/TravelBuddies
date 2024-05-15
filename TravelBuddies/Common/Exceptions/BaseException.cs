namespace TravelBuddies.Application.Common.Exceptions
{
    public abstract class BaseException : Exception
    {
        public BaseException(string message)
            : base(message)
        {

        }

        public BaseException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
