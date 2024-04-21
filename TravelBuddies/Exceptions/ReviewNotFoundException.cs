namespace TravelBuddies.Application.Exceptions
{
	public class ReviewNotFoundException : BaseException
	{
		public ReviewNotFoundException(string message) : base(message)
		{
		}

		public ReviewNotFoundException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
