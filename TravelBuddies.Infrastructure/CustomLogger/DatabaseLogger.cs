namespace TravelBuddies.Infrastructure.CustomLogger
{
    using MediatR;
    using System.Threading.Tasks;
    using TravelBuddies.Application.Common.Interfaces.CustomLogger;
    using TravelBuddies.Application.Logger.Commands.CreateLog;
    using TravelBuddies.Domain.Enums;

    public class DatabaseLogger : ILogger
	{
		private readonly IMediator _mediator;
        private readonly LogLevel _loggerValue;

		public DatabaseLogger(IMediator mediator, LogLevel loggerValue)
        {
            _mediator = mediator;
            _loggerValue = loggerValue;
        }

        public async Task LogAsync(LogLevel level, string message)
		{
			if (_loggerValue <= level)
            { 
				await _mediator.Send(new CreateLogCommand(message, level));
            }
			
		}
	}
}
