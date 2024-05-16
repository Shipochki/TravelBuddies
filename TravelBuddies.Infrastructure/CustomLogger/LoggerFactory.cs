namespace TravelBuddies.Infrastructure.CustomLogger
{
    using MediatR;
    using TravelBuddies.Application.Common.Interfaces.CustomLogger;

    public class LoggerFactory : ILoggerFactory
	{
		private readonly string _filePath;
		private readonly IMediator _mediator;
		private readonly string _loggerValue;

        public LoggerFactory(string filePath, IMediator mediator, string loggerValue = "Information")
        {
            _filePath = filePath;
			_mediator = mediator;
			_loggerValue = loggerValue;
        }

		public ILogger CreateDatabaseLoggerAsync()
		{
			return new DatabaseLogger(_mediator, _loggerValue);
		}

		public ILogger CreateFileLoggerAsync(string categoryName)
		{
			return new FileLogger(categoryName, _filePath, _loggerValue);
		}
	}
}
