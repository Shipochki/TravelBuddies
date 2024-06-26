﻿namespace TravelBuddies.Presentation.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using TravelBuddies.Infrastructure.CustomLogger;
    using TravelBuddies.Domain.Common;
    using TravelBuddies.Application.Common.Interfaces.CustomLogger;
	using TravelBuddies.Domain.Enums;

	[Route("api/[controller]")]
	[ApiController]
	public abstract class BaseController : ControllerBase
	{
        protected readonly IMediator _mediator;
        protected readonly ILogger _fileLogger;
        protected readonly ILogger _databaseLogger;


        public BaseController(IMediator mediator)
        { 
            _mediator = mediator;

            LoggerFactory loggerFactory = new LoggerFactory(ApplicationLogsFilePaths.Logs, _mediator, LogLevel.Error);
            _fileLogger = loggerFactory.CreateFileLoggerAsync(ApplicationCategoryNames.FileLogger);
            _databaseLogger = loggerFactory.CreateDatabaseLoggerAsync();
        }
    }
}
