namespace TravelBuddies.Application.CustomLogger.Interfaces
{
	public interface ILoggerFactory
	{
		public Task<Logger> CreateLoggerAsync(string categoryName);
	}
}
