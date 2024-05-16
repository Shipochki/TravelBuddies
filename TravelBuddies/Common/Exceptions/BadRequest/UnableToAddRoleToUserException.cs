namespace TravelBuddies.Application.Common.Exceptions.BadRequest
{
    public class UnableToAddRoleToUserException : BadRequestBaseException
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
