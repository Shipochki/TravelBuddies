namespace TravelBuddies.Application.Common.Exceptions.BadRequest
{
    public class UnableToCreateApplicationUserException : BadRequestBaseException
    {
        public UnableToCreateApplicationUserException(string message) : base(message)
        {
        }

        public UnableToCreateApplicationUserException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
