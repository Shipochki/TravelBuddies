namespace TravelBuddies.Presentation.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TravelBuddies.Domain.Enums;
    using TravelBuddies.Domain.Common;
    using TravelBuddies.Presentation.DTOs.User;
    using TravelBuddies.Application.User.Commands.CreateApplicationUser;
    using TravelBuddies.Application.User.Commands.BecomeDriver;
    using TravelBuddies.Application.User.Commands.DeleteApplicationUser;
    using Microsoft.AspNetCore.Cors;
    using TravelBuddies.Application.User.Commands.LoginApplicationUser;
    using TravelBuddies.Presentation.Filters;
    using TravelBuddies.Domain.Entities;
    using TravelBuddies.Application.User.Queries.GetUserById;
    using TravelBuddies.Application.Review.Queries.GetReviewsByReciverId;
    using TravelBuddies.Presentation.DTOs.Review;
    using TravelBuddies.Application.Vehicle.Queries.GetVehicleByOwnerId;
    using TravelBuddies.Presentation.DTOs.Vehicle;
    using TravelBuddies.Presentation.Extensions;

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
		//[ModelStateValidation]
		public async Task<IActionResult> Register([FromForm] UserRegisterDto userRegisterDto)
		{
			var formCollection = Request.Form["profilepicture"];

			CreateApplicationUserCommand command = new CreateApplicationUserCommand()
			{
				Email = userRegisterDto.Email,
				FirstName = userRegisterDto.FirstName,
				LastName = userRegisterDto.LastName,
				City = userRegisterDto.City,
				Country = userRegisterDto.Country,
				Password = userRegisterDto.Password,
				ProfilePicture = userRegisterDto.ProfilePicture,
			};

			await _mediator.Send(command);

			LogLevel logLevel = LogLevel.Information;
			string message = "User created successfuly.";

			await _fileLogger.LogAsync(logLevel, message);
			await _databaseLogger.LogAsync(logLevel, message);

			return Created();
		}

		[HttpPost]
		[Authorize(Policy = ApplicationPolicies.OnlyClient)]
		[Route("[action]")]
		public async Task<IActionResult> BecomeDriver()
		{
			await _mediator.Send(new BecomeDriverCommand(User.Id()));

			LogLevel logLevel = LogLevel.Information;
			string message = "Succesfully became Driver";

			await _fileLogger.LogAsync(logLevel, message);
			await _databaseLogger.LogAsync(logLevel, message);

			return Ok(message);
		}

		[HttpPost]
		[Authorize]
		[Route("[action]")]
		public async Task<IActionResult> Delete()
		{
			await _mediator.Send(new DeleteApplicationUserCommand(User.Id()));

			LogLevel logLevel = LogLevel.Information;
			string message = "Succesfully deleted user";

			await _fileLogger.LogAsync(logLevel, message);
			await _databaseLogger.LogAsync(logLevel, message);

			return Ok(message);
		}

		[HttpPost]
		[Route("[action]")]
		//[ModelStateValidation]
		[AllowAnonymous]
		public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
		{
			string token = await _mediator.Send(
				new LoginApplicationUserCommand(userLoginDto.Email, userLoginDto.Password));

			LogLevel logLevel = LogLevel.Information;
			string message = "Succesfully login user";

			await _fileLogger.LogAsync(logLevel, message);
			await _databaseLogger.LogAsync(logLevel, message);

			return Ok(new { token });
		}

		[HttpGet]
		[Authorize]
		[Route("[action]/{id}")]
		public async Task<IActionResult> GetUserById(string id)
		{
			ApplicationUser user = await _mediator.Send(new GetUserByIdQuery(id));

			GetUserByIdDto userDto = GetUserByIdDto.FromUser(user);

			List<Review> reviews = await _mediator.Send(new GetReviewsByReciverIdQuery(id));

			userDto.Reviews = reviews.Select(ReviewDto.FromReview).ToList();
			
			Vehicle? vehicle = await _mediator.Send(new GetVehicleByOwnerIdQuery(id));

			if(vehicle != null)
			{
				userDto.Vehicle = VehicleDto.FromVehicle(vehicle);
			}
			
			LogLevel logLevel = LogLevel.Information;
			string message = "Succesfully get user";

			await _fileLogger.LogAsync(logLevel, message);
			await _databaseLogger.LogAsync(logLevel, message);

			return Ok(userDto);
		}
	}
}
