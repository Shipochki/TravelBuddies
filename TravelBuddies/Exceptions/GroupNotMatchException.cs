namespace TravelBuddies.Application.Exceptions
{
	public class GroupNotMatchException : BaseException
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
