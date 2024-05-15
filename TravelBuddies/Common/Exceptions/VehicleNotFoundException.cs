namespace TravelBuddies.Application.Common.Exceptions
{
    public class VehicleNotFoundException : BaseException
    {
        public VehicleNotFoundException(string message) : base(message)
        {
        }

        public VehicleNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
