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
        private readonly string _loggerValue;

		public DatabaseLogger(IMediator mediator, string loggerValue)
        {
            _mediator = mediator;
            _loggerValue = loggerValue;
        }

        public async Task LogAsync(LogLevel level, string message)
		{
			LogLevel parsedLevel;
			Enum.TryParse(_loggerValue, out parsedLevel);
			if (((int)parsedLevel) <= ((int)level))
            { 
				await _mediator.Send(new CreateLogCommand(message, level));
            }
			
		}
	}
}
