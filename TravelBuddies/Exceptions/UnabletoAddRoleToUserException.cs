namespace TravelBuddies.Application.Exceptions
{
	public class UnableToAddRoleToUserException : BaseException
	{
		public UnableToAddRoleToUserException(string message)
			: base(message)
		{
		}

		public UnableToAddRoleToUserException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
