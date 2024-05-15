namespace TravelBuddies.Application.Common.Exceptions
{
    public class NotAvailableSeatsInPostException : BaseException
    {
        public NotAvailableSeatsInPostException(string message) : base(message)
        {
        }

        public NotAvailableSeatsInPostException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
