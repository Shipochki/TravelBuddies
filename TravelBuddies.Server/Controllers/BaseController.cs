namespace TravelBuddies.Presentation.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using TravelBuddies.Application.CustomLogger;
    using TravelBuddies.Application.CustomLogger.Interfaces;
	using TravelBuddies.Presentation.Constants;

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

            LoggerFactory loggerFactory = new LoggerFactory(ApplicationLogsFilePaths.Logs, _mediator);
            _fileLogger = loggerFactory.CreateFileLoggerAsync(ApplicationCategoryNames.FileLogger);
            _databaseLogger = loggerFactory.CreateDatabaseLoggerAsync();
        }
    }
}
