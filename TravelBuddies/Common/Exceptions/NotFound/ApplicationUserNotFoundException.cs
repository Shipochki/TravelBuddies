namespace TravelBuddies.Application.Common.Exceptions.NotFound
{
    public class ApplicationUserNotFoundException : NotFoundBaseException
    {
        public ApplicationUserNotFoundException(string message)
            : base(message)
        {

        }

        public ApplicationUserNotFoundException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
