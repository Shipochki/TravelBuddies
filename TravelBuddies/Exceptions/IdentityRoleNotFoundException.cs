namespace TravelBuddies.Application.Exceptions
{
	public class IdentityRoleNotFoundException : BaseException
	{
		public IdentityRoleNotFoundException(string message) : base(message)
		{
		}

		public IdentityRoleNotFoundException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
