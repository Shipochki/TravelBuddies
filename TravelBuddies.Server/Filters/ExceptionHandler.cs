﻿namespace TravelBuddies.Presentation.Filters
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc.Filters;
    using TravelBuddies.Infrastructure.CustomLogger;
    using TravelBuddies.Domain.Common;
    using TravelBuddies.Domain.Enums;
    using Microsoft.AspNetCore.Mvc;
    using TravelBuddies.Presentation.Responses;
    using TravelBuddies.Application.Common.Exceptions;
    using TravelBuddies.Application.Common.Interfaces.CustomLogger;

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

			if(context.Exception is ApplicationUserNotFoundException
				|| context.Exception is CityNotFoundException
				|| context.Exception is GroupNotFoundException
				|| context.Exception is IdentityRoleNotFoundException
				|| context.Exception is MessageNotFoundException
				|| context.Exception is PostNotFoundException
				|| context.Exception is ReviewNotFoundException
				|| context.Exception is VehicleNotFoundException
				|| context.Exception is ApplicationUserNotInGroupException
				)
			{
				status = 404;
				statusPhrase = "Not Found";
			}
			else if(context.Exception is ApplicationUserNotCreatorException)
			{
				status = 403;
				statusPhrase = "Forbidden";
			}
			else if(context.Exception is UnableToCreateApplicationUserException
				|| context.Exception is UnableToAddRoleToUserException
				|| context.Exception is InvalidLoginException
				|| context.Exception is ApplicationUserAllreadyInGroupException
				|| context.Exception is NotAvailableSeatsInPostException
				|| context.Exception is GroupNotMatchException)
			{
				status = 400;
				statusPhrase = "Bad Request";
			}

			ErrorResponse error = new ErrorResponse()
			{
				StatusCode = status,
				StatusPhrase = statusPhrase,
				Timestamp = DateTime.Now,
				ErrorMessage = new List<string> { message }
			};

			_fileLogger.LogAsync(logLevel, message);
			_databaseLogger.LogAsync(logLevel, message);
			context.Result = new JsonResult(error) { StatusCode = status};
		}
	}
}
