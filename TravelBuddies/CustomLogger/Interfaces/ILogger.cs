namespace TravelBuddies.Application.CustomLogger.Interfaces
{
	using TravelBuddies.Domain.Enums;

    public interface ILogger
	{
		public Task LogAsync(LogLevel level, string message);
	}
}
