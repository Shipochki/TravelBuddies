namespace TravelBuddies.Application.Exceptions
{
	public class ApplicationUserNotCreatorException : BaseException
	{
        public ApplicationUserNotCreatorException(string message)
            : base(message) 
        {
        }

        public ApplicationUserNotCreatorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
