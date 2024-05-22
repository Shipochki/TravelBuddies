using TravelBuddies.Application.Common.Exceptions.NotFound;

namespace TravelBuddies.Application.Common.Exceptions.Forbidden
{
    public class ApplicationUserNotInGroupException : ForbiddenBaseException
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
