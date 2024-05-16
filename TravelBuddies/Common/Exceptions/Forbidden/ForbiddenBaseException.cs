namespace TravelBuddies.Application.Common.Exceptions.Forbidden
{
	public class ForbiddenBaseException : BaseException
	{
		public ForbiddenBaseException(string message) : base(message)
		{
		}

		public ForbiddenBaseException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
