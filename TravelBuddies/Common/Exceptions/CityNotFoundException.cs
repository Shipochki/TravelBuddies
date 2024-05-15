namespace TravelBuddies.Application.Common.Exceptions
{
    public class CityNotFoundException : BaseException
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
