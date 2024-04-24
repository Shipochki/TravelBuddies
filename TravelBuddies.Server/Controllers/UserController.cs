namespace TravelBuddies.Presentation.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TravelBuddies.Domain.Enums;
    using TravelBuddies.Application.Constants;
    using TravelBuddies.Presentation.DTOs.User;
	using TravelBuddies.Application.User.Commands.CreateApplicationUser;
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.User.Commands.BecomeDriver;
	using TravelBuddies.Application.User.Commands.DeleteApplicationUser;

	[Route("api/[controller]")]
	[ApiController]
	public class UserController : BaseController
	{
		public UserController(IMediator mediator) 
			: base(mediator)
		{
		}

		[HttpPost]
		[AllowAnonymous]
		[Route("[action]")]
		public async Task<IActionResult> Register([FromBody]UserRegisterDto userRegisterDto)
		{
			CreateApplicationUserCommand command = new CreateApplicationUserCommand()
			{
				Email = userRegisterDto.Email,
				FirstName = userRegisterDto.FirstName,
				LastName = userRegisterDto.LastName,
				City = userRegisterDto.City,
				Country = userRegisterDto.Country,
				Password = userRegisterDto.Password,
			};

			if(!ModelState.IsValid) 
			{
				return BadRequest(ModelState);
			}

			LogLevel logLevel;
			string message;

			try
			{
				await _mediator.Send(command);

				logLevel = LogLevel.Information;
				message = "User created successfuly.";

				await _fileLogger.LogAsync(logLevel, message);
				await _databaseLogger.LogAsync(logLevel, message);
				
				return Created();
			}
			catch (UnableToCreateApplicationUserException m)
			{
				logLevel = LogLevel.Error;

				await _fileLogger.LogAsync(logLevel, m.Message);
				await _databaseLogger.LogAsync(logLevel, m.Message);

				return BadRequest(m.Message);
			}
		}

		[HttpPost]
		[Authorize(Policy = ApplicationPolicies.OnlyClient)]
		[Route("[action]")]
		public async Task<IActionResult> BecomeDriver(string userId)
		{
			LogLevel logLevel;
			string message;

			try
			{
				await _mediator.Send(new BecomeDriverCommand(userId));

				logLevel = LogLevel.Information;
				message = "Succesfully became Driver";

				await _fileLogger.LogAsync(logLevel, message);
				await _databaseLogger.LogAsync(logLevel, message);

				return Ok(message);
			}
			catch (ApplicationUserNotFoundException m)
			{
				logLevel = LogLevel.Error;

				await _fileLogger.LogAsync(logLevel, m.Message);
				await _databaseLogger.LogAsync(logLevel, m.Message);

				return NotFound(m.Message);
			}
			catch (IdentityRoleNotFoundException m)
			{
				logLevel = LogLevel.Error;

				await _fileLogger.LogAsync(logLevel, m.Message);
				await _databaseLogger.LogAsync(logLevel, m.Message);

				return NotFound(m.Message);
			}
			catch (UnableToAddRoleToUserException m)
			{
				logLevel = LogLevel.Error;

				await _fileLogger.LogAsync(logLevel, m.Message);
				await _databaseLogger.LogAsync(logLevel, m.Message);

				return BadRequest(m.Message);
			}
		}

		[HttpPost]
		[Authorize]
		[Route("[action]")]
		public async Task<IActionResult> Delete(string userId)
		{
			LogLevel logLevel;
			string message;

			try
			{
				await _mediator.Send(new DeleteApplicationUserCommand(userId));

				logLevel = LogLevel.Information;
				message = "Succesfully deleted user";

				await _fileLogger.LogAsync(logLevel, message);
				await _databaseLogger.LogAsync(logLevel, message);

				return Ok(message);
			}
			catch (ApplicationUserNotFoundException m)
			{
				logLevel = LogLevel.Error;

				await _fileLogger.LogAsync(logLevel, m.Message);
				await _databaseLogger.LogAsync(logLevel, m.Message);

				return NotFound(m.Message);
			}
		}
	}
}
