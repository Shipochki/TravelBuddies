namespace TravelBuddies.Application.Common.Exceptions
{
    public class GroupNotFoundException : BaseException
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
