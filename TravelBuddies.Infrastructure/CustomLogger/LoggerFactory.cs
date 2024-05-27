namespace TravelBuddies.Infrastructure.CustomLogger
{
    using MediatR;
    using TravelBuddies.Application.Common.Interfaces.CustomLogger;
	using TravelBuddies.Domain.Enums;

	public class LoggerFactory : ILoggerFactory
	{
		private readonly string _filePath;
		private readonly IMediator _mediator;
		private readonly LogLevel _loggerValue;

        public LoggerFactory(string filePath, IMediator mediator, LogLevel loggerValue = LogLevel.Information)
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
