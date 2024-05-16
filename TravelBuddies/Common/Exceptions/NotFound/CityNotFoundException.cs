namespace TravelBuddies.Application.Common.Exceptions.NotFound
{
    public class CityNotFoundException : NotFoundBaseException
    {
        public CityNotFoundException(string message)
            : base(message)
        {
        }

        public CityNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
