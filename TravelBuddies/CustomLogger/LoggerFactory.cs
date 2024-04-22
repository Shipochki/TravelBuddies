namespace TravelBuddies.Application.CustomLogger
{
	using MediatR;
	using TravelBuddies.Application.CustomLogger.Interfaces;

	public class LoggerFactory : ILoggerFactory
	{
		private readonly string _filePath;

        public LoggerFactory(string filePath)
        {
            _filePath = filePath;
        }

		public Task<DatabaseLogger> CreateDatabaseLoggerAsync(IMediator mediator)
		{
			return Task.FromResult(new DatabaseLogger(mediator));
		}

		public Task<FileLogger> CreateFileLoggerAsync(string categoryName)
		{
			return Task.FromResult(new FileLogger(categoryName, _filePath));
		}
	}
}
