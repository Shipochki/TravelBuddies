
namespace TravelBuddies.Application.CustomLogger
{
	using TravelBuddies.Application.CustomLogger.Interfaces;

	public class LoggerFactory : ILoggerFactory
	{
		private readonly string _filePath;

        public LoggerFactory(string filePath)
        {
            _filePath = filePath;
        }

        public Task<Logger> CreateLoggerAsync(string categoryName)
		{
			return Task.FromResult(new Logger(categoryName, _filePath));
		}
	}
}
