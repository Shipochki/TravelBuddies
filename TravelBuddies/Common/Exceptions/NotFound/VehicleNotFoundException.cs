namespace TravelBuddies.Application.Common.Exceptions.NotFound
{
    public class VehicleNotFoundException : NotFoundBaseException
    {
        public VehicleNotFoundException(string message) : base(message)
        {
        }

        public VehicleNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
