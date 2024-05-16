namespace TravelBuddies.Application.Common.Exceptions.Forbidden
{
    public class ApplicationUserNotCreatorException : ForbiddenBaseException
    {
        public ApplicationUserNotCreatorException(string message)
            : base(message)
        {
        }

        public ApplicationUserNotCreatorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
