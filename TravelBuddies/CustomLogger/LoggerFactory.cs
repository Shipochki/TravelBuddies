namespace TravelBuddies.Application.CustomLogger
{
	using MediatR;
	using TravelBuddies.Application.CustomLogger.Interfaces;

	public class LoggerFactory : ILoggerFactory
	{
		private readonly string _filePath;
		private readonly IMediator _mediator;

        public LoggerFactory(string filePath, IMediator mediator)
        {
            _filePath = filePath;
			_mediator = mediator;
        }

		public DatabaseLogger CreateDatabaseLoggerAsync()
		{
			return new DatabaseLogger(_mediator);
		}

		public FileLogger CreateFileLoggerAsync(string categoryName)
		{
			return new FileLogger(categoryName, _filePath);
		}
	}
}
