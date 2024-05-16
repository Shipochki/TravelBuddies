namespace TravelBuddies.Application.Common.Exceptions.BadRequest
{
    public class NotAvailableSeatsInPostException : BadRequestBaseException
    {
        public NotAvailableSeatsInPostException(string message) : base(message)
        {
        }

        public NotAvailableSeatsInPostException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
