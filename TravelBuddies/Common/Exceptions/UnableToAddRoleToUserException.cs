namespace TravelBuddies.Application.Common.Exceptions
{
    public class UnableToAddRoleToUserException : BaseException
    {
        public UnableToAddRoleToUserException(string message)
            : base(message)
        {
        }

        public UnableToAddRoleToUserException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
