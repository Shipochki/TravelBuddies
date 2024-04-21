
namespace TravelBuddies.Application.Exceptions
{
	public class ApplicationUserAllreadyInGroupException : BaseException
	{
		public ApplicationUserAllreadyInGroupException(string message) : base(message)
		{
		}

		public ApplicationUserAllreadyInGroupException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
