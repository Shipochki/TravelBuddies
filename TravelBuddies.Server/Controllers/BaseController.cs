namespace TravelBuddies.Presentation.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using TravelBuddies.Application.CustomLogger;
    using TravelBuddies.Application.CustomLogger.Interfaces;

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
            LoggerFactory loggerFactory = new LoggerFactory("Logs", _mediator);
            _fileLogger = loggerFactory.CreateFileLoggerAsync("FileLogger");
            _databaseLogger = loggerFactory.CreateDatabaseLoggerAsync();
        }
    }
}
