namespace TravelBuddies.Application.Common.Exceptions.BadRequest
{
	public class ApplicationUserAllreadyIsBannedFromGroupException : BadRequestBaseException
	{
		public ApplicationUserAllreadyIsBannedFromGroupException(string message) : base(message)
		{
		}

		public ApplicationUserAllreadyIsBannedFromGroupException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
