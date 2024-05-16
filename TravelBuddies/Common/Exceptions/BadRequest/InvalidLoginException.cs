namespace TravelBuddies.Application.Common.Exceptions.BadRequest
{
    public class InvalidLoginException : BadRequestBaseException
    {
        public InvalidLoginException(string message) : base(message)
        {
        }

        public InvalidLoginException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
