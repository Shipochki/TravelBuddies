namespace TravelBuddies.Infrastructure.CustomLogger
{
    using MediatR;
    using TravelBuddies.Application.Interfaces.CustomLogger;

    public class LoggerFactory : ILoggerFactory
	{
		private readonly string _filePath;
		private readonly IMediator _mediator;

        public LoggerFactory(string filePath, IMediator mediator)
        {
            _filePath = filePath;
			_mediator = mediator;
        }

		public ILogger CreateDatabaseLoggerAsync()
		{
			return new DatabaseLogger(_mediator);
		}

		public ILogger CreateFileLoggerAsync(string categoryName)
		{
			return new FileLogger(categoryName, _filePath);
		}
	}
}
