namespace TravelBuddies.Application.Common.Exceptions.BadRequest
{
	public class BadRequestBaseException : BaseException
	{
		public BadRequestBaseException(string message) : base(message)
		{
		}

		public BadRequestBaseException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
