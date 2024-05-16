namespace TravelBuddies.Application.Common.Exceptions.NotFound
{
    public class ApplicationUserNotInGroupException : NotFoundBaseException
    {
        public ApplicationUserNotInGroupException(string message)
            : base(message)
        {
        }

        public ApplicationUserNotInGroupException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
