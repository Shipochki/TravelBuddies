namespace TravelBuddies.Presentation.Filters
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc.Filters;
    using TravelBuddies.Infrastructure.CustomLogger;
    using TravelBuddies.Domain.Common;
    using TravelBuddies.Domain.Enums;
    using Microsoft.AspNetCore.Mvc;
    using TravelBuddies.Application.Common.Interfaces.CustomLogger;
    using TravelBuddies.Application.Common.Exceptions.NotFound;
    using TravelBuddies.Application.Common.Exceptions.Forbidden;
    using TravelBuddies.Application.Common.Exceptions.BadRequest;

    public class ExceptionHandler : IExceptionFilter
	{
		protected readonly IMediator _mediator;
		protected readonly ILogger _fileLogger;
		protected readonly ILogger _databaseLogger;

		public ExceptionHandler(IMediator mediator)
		{
			_mediator = mediator;
			LoggerFactory loggerFactory = new LoggerFactory(ApplicationLogsFilePaths.Logs, _mediator);
			_fileLogger = loggerFactory.CreateFileLoggerAsync(ApplicationCategoryNames.FileLogger);
			_databaseLogger = loggerFactory.CreateDatabaseLoggerAsync();
		}

		public void OnException(ExceptionContext context)
		{
			LogLevel logLevel = LogLevel.Error;
			int status = 500;
			string statusPhrase = "Internal Server Error";
			string message = context.Exception.Message;
			string detailMessage = string.Empty;

			if(context.Exception is NotFoundBaseException)
			{
				status = 404;
				statusPhrase = "Not Found";
			}
			else if(context.Exception is ForbiddenBaseException)
			{
				status = 403;
				statusPhrase = "Forbidden";
			}
			else if(context.Exception is BadRequestBaseException)
			{
				status = 400;
				statusPhrase = "Bad Request";
			}


			detailMessage += message + Environment.NewLine;
			if (context.Exception is AggregateException aggregateException)
			{
				foreach (var innerException in aggregateException.InnerExceptions)
				{
					detailMessage += innerException.Message + Environment.NewLine;
				}
			}

			ProblemDetails problemDetails = new ProblemDetails
			{
				Status = status,
				Title = statusPhrase,
				Detail = detailMessage.TrimEnd(),
				Instance = context.HttpContext.Request.Path
			};

			_fileLogger.LogAsync(logLevel, message);
			_databaseLogger.LogAsync(logLevel, message);
			context.Result = new JsonResult(problemDetails) { StatusCode = status};
		}
	}
}
