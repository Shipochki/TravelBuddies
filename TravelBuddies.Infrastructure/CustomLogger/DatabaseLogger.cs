﻿namespace TravelBuddies.Infrastructure.CustomLogger
{
    using MediatR;
    using System.Threading.Tasks;
    using TravelBuddies.Application.Interfaces.CustomLogger;
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
			await _mediator.Send(new CreateLogCommand(message, level));
		}
	}
}
