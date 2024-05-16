namespace TravelBuddies.Application.Common.Exceptions.BadRequest
{
    public class ApplicationUserAllreadyInGroupException : BadRequestBaseException
    {
        public ApplicationUserAllreadyInGroupException(string message) : base(message)
        {
        }

        public ApplicationUserAllreadyInGroupException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
