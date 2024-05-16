namespace TravelBuddies.Application.Common.Exceptions.NotFound
{
    public class GroupNotFoundException : NotFoundBaseException
    {
        public GroupNotFoundException(string message)
            : base(message)
        {
        }

        public GroupNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
