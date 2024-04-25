namespace TravelBuddies.Application.Exceptions
{
	public class InvalidLoginException : BaseException
	{
		public InvalidLoginException(string message) : base(message)
		{
		}

		public InvalidLoginException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
