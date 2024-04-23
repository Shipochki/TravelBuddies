namespace TravelBuddies.Presentation.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using TravelBuddies.Domain.Entities;
    using TravelBuddies.Domain.Enums;
    using TravelBuddies.Application.Constants;
    using TravelBuddies.Presentation.DTOs.User;
	using TravelBuddies.Application.User.Commands.CreateApplicationUser;
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.User.Commands.BecomeDriver;

	[Route("api/[controller]")]
	[ApiController]
	public class UserController : BaseController
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public UserController(
			IMediator mediator
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager) : base(mediator)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			//SeedDb().Wait();
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

			LogLevel logLevel;
			string message;

			try
			{
				await _mediator.Send(command);

				logLevel = LogLevel.Information;
				message = "User created successfuly.";

				await _fileLogger.LogAsync(logLevel, message);
				await _databaseLogger.LogAsync(logLevel, message);
				
				return Ok(message);
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

				return BadRequest(m.Message);
			}
			catch (IdentityRoleNotFoundException m)
			{
				logLevel = LogLevel.Error;

				await _fileLogger.LogAsync(logLevel, m.Message);
				await _databaseLogger.LogAsync(logLevel, m.Message);

				return BadRequest(m.Message);
			}
			catch (UnableToAddRoleToUserException m)
			{
				logLevel = LogLevel.Error;

				await _fileLogger.LogAsync(logLevel, m.Message);
				await _databaseLogger.LogAsync(logLevel, m.Message);

				return BadRequest(m.Message);
			}
		}

		private async Task SeedDb()
		{
			// Ensure "client" role exists
			if (!await _roleManager.RoleExistsAsync(ApplicationRoles.Client))
			{
				await _roleManager.CreateAsync(new IdentityRole(ApplicationRoles.Client));
			}

			// Ensure "driver" role exists
			if (!await _roleManager.RoleExistsAsync(ApplicationRoles.Driver))
			{
				await _roleManager.CreateAsync(new IdentityRole(ApplicationRoles.Driver));
			}

			// Ensure "admin" role exists
			if (!await _roleManager.RoleExistsAsync(ApplicationRoles.Admin))
			{
				await _roleManager.CreateAsync(new IdentityRole(ApplicationRoles.Admin));
			}

			// Ensure "admin" user exists
			if(await _userManager.FindByEmailAsync("admin@gmail.com") == null)
			{
				ApplicationUser applicationUser = new ApplicationUser()
				{
					UserName = "admin@gmail.com",
					Email = "admin@gmail.com",
					FirstName = "Admin",
					LastName = "Administrator"
				};

				await _userManager.CreateAsync(applicationUser, "Password0!");

				await _userManager.AddToRoleAsync(applicationUser, ApplicationRoles.Admin);
			}
		}
	}
}
