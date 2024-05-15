namespace TravelBuddies.Application.Common.Interfaces.CustomLogger
{
    using TravelBuddies.Domain.Enums;

    public interface ILogger
    {
        public Task LogAsync(LogLevel level, string message);
    }
}
