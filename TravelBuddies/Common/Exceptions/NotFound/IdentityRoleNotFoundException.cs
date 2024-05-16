namespace TravelBuddies.Application.Common.Exceptions.NotFound
{
    public class IdentityRoleNotFoundException : NotFoundBaseException
    {
        public IdentityRoleNotFoundException(string message) : base(message)
        {
        }

        public IdentityRoleNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
