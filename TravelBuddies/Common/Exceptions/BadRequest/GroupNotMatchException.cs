namespace TravelBuddies.Application.Common.Exceptions.BadRequest
{
    public class GroupNotMatchException : BadRequestBaseException
    {
        public GroupNotMatchException(string message)
            : base(message)
        {
        }

        public GroupNotMatchException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
