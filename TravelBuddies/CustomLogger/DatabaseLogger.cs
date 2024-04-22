namespace TravelBuddies.Application.CustomLogger
{
	using MediatR;
	using System.Threading.Tasks;
	using TravelBuddies.Application.CustomLogger.Interfaces;
	using TravelBuddies.Application.Logger.Commands.CreateLog;
	using TravelBuddies.Domain.Enums;

	public class DatabaseLogger : ILogger
	{
		private readonly IMediator _mediator;

		public DatabaseLogger(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task LogAsync(LogLevel level, string message)
		{
			CreateLogCommand command = new CreateLogCommand(message, level);
			await _mediator.Send(command);
		}
	}
}
