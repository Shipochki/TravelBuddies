namespace TravelBuddies.Presentation.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TravelBuddies.Domain.Enums;
    using TravelBuddies.Domain.Constants;
    using TravelBuddies.Presentation.DTOs.User;
	using TravelBuddies.Application.User.Commands.CreateApplicationUser;
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.User.Commands.BecomeDriver;
	using TravelBuddies.Application.User.Commands.DeleteApplicationUser;
	using Microsoft.AspNetCore.Cors;
	using TravelBuddies.Application.User.Commands.LoginApplicationUser;

	[EnableCors(ApplicationCorses.AllowOrigin)]
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

			LogLevel logLevel = LogLevel.Error;

			try
			{
				await _mediator.Send(command);

				logLevel = LogLevel.Information;
				string message = "User created successfuly.";

				await _fileLogger.LogAsync(logLevel, message);
				await _databaseLogger.LogAsync(logLevel, message);
				
				return Created();
			}
			catch (UnableToCreateApplicationUserException m)
			{
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
			LogLevel logLevel = LogLevel.Error;

			try
			{
				await _mediator.Send(new BecomeDriverCommand(userId));

				logLevel = LogLevel.Information;
				string message = "Succesfully became Driver";

				await _fileLogger.LogAsync(logLevel, message);
				await _databaseLogger.LogAsync(logLevel, message);

				return Ok(message);
			}
			catch (ApplicationUserNotFoundException m)
			{
				await _fileLogger.LogAsync(logLevel, m.Message);
				await _databaseLogger.LogAsync(logLevel, m.Message);

				return NotFound(m.Message);
			}
			catch (IdentityRoleNotFoundException m)
			{
				await _fileLogger.LogAsync(logLevel, m.Message);
				await _databaseLogger.LogAsync(logLevel, m.Message);

				return NotFound(m.Message);
			}
			catch (UnableToAddRoleToUserException m)
			{
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
			LogLevel logLevel = LogLevel.Error;

			try
			{
				await _mediator.Send(new DeleteApplicationUserCommand(userId));

				logLevel = LogLevel.Information;
				string message = "Succesfully deleted user";

				await _fileLogger.LogAsync(logLevel, message);
				await _databaseLogger.LogAsync(logLevel, message);

				return Ok(message);
			}
			catch (ApplicationUserNotFoundException m)
			{
				await _fileLogger.LogAsync(logLevel, m.Message);
				await _databaseLogger.LogAsync(logLevel, m.Message);

				return NotFound(m.Message);
			}
		}

		[HttpPost]
		[AllowAnonymous]
		[Route("[action]")]
		public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
		{
			if(!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			LogLevel logLevel = LogLevel.Error;

			try
			{
				string token = await _mediator.Send(
					new LoginApplicationUserCommand(userLoginDto.Email, userLoginDto.Password));

				logLevel = LogLevel.Information;
				string message = "Succesfully login user";

				await _fileLogger.LogAsync(logLevel, message);
				await _databaseLogger.LogAsync(logLevel, message);

				return Ok(new { token });
			}
			catch (InvalidLoginException m)
			{
				await _fileLogger.LogAsync(logLevel, m.Message);
				await _databaseLogger.LogAsync(logLevel, m.Message);

				return BadRequest(m.Message);
			}
		}
	}
}
