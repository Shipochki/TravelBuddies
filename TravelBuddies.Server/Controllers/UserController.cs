namespace TravelBuddies.Presentation.Controllers
{
	using MediatR;
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
			SeedDb().Wait();
		}

		[HttpPost]
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

			if(result.Succeeded)
			{
				LogLevel loglevel = LogLevel.Information;
				string message = "User created successfuly.";

				await _fileLogger.LogAsync(loglevel, message);
				await _databaseLogger.LogAsync(loglevel, message);

				await _userManager.AddToRoleAsync(applicationUser, ApplicationRoles.Client);

				return Ok(message);
			}
			else
			{
				LogLevel loglevel = LogLevel.Error;
				string message = "User is not created successfuly.";

				await _fileLogger.LogAsync(loglevel, message);
				await _databaseLogger.LogAsync(loglevel, message);

				return BadRequest(result.Errors);
			}
		}

		private async Task SeedDb()
		{
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

			if(await _userManager.FindByEmailAsync("admin@gmail.com") == null)
			{
				ApplicationUser applicationUser = new ApplicationUser()
				{
					UserName = "admin@gmail.com",
					Email = "admin@gmail.com",
					FirstName = "Admin",
					LastName = "Administrator"
				};

				IdentityResult result = await _userManager.CreateAsync(applicationUser, "Password0!");
			}
		}
	}
}
