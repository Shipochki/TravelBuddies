using MediatR;
using TravelBuddies.Application.CustomLogger.Enums;

namespace TravelBuddies.Application.CustomLogger.Interfaces
{
    public interface ILogger
	{
		public Task LogAsync(LogLevel level, string message);
	}
}
