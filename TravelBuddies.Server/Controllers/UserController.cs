namespace TravelBuddies.Presentation.Controllers
{
	using MediatR;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Mvc;
	using TravelBuddies.Domain.Entities;
	using TravelBuddies.Domain.Enums;
	using TravelBuddies.Presentation.Constants;
	using TravelBuddies.Presentation.DTOs;

	[Route("api/[controller]")]
	[ApiController]
	public class UserController : BaseController
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public UserController(IMediator mediator
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
			ApplicationUser applicationUser = new ApplicationUser()
			{
				UserName = userRegisterDto.Email,
				Email = userRegisterDto.Email,
				FirstName = userRegisterDto.FirstName,
				LastName = userRegisterDto.LastName,
				City = userRegisterDto.City,
				Country = userRegisterDto.Country,
			};

			IdentityResult result = await _userManager.CreateAsync(applicationUser, userRegisterDto.Password);

			LogLevel logLevel;
			string message;

			if(result.Succeeded)
			{
				logLevel = LogLevel.Information;
				message = "User created successfuly.";

				await _fileLogger.LogAsync(logLevel, message);
				await _databaseLogger.LogAsync(logLevel, message);

				await _userManager.AddToRoleAsync(applicationUser, ApplicationRoles.Client);

				return Ok(message);
			}
			else
			{
				logLevel = LogLevel.Error;
				message = "User is not created successfuly.";

				await _fileLogger.LogAsync(logLevel, message);
				await _databaseLogger.LogAsync(logLevel, message);

				return BadRequest(result.Errors);
			}
		}

		[HttpPost]
		[Authorize(Policy = ApplicationPolicies.OnlyClient)]
		[Route("[action]")]
		public async Task<IActionResult> BecomeDriver(string userId)
		{
			ApplicationUser? applicationUser = await _userManager.FindByIdAsync(userId);

			if(applicationUser == null)
			{
				return BadRequest("User does not exist");
			}

			IdentityRole? identityRole = await _roleManager.FindByNameAsync(ApplicationRoles.Driver);

			if(identityRole == null)
			{
				return BadRequest("Something gone wrong");
			}

			var result = await _userManager.AddToRoleAsync(applicationUser, ApplicationRoles.Driver);

			if(!result.Succeeded)
			{
				return BadRequest("Failed to become Driver");
			}

			return Ok("Succesfully became Driver");
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
