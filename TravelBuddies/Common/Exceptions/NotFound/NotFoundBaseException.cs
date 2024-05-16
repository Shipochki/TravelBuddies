namespace TravelBuddies.Application.Common.Exceptions.NotFound
{
	public abstract class NotFoundBaseException : BaseException
	{
		public NotFoundBaseException(string message) : base(message)
		{
		}

		public NotFoundBaseException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
