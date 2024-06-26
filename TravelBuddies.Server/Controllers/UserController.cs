﻿namespace TravelBuddies.Presentation.Controllers
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
	using TravelBuddies.Application.User.Commands.UpdateProfilePicture;
	using TravelBuddies.Application.User.Commands.UpdateApplicationUser;
	using TravelBuddies.Application.Review.Queries.GetReviewsAvgRatingByReciverId;

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
		public async Task<IActionResult> Register([FromForm] UserRegisterDto userRegisterDto)
		{
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

			List<Review> reviews = await _mediator.Send(new GetReviewsByReciverIdQuery(id, 1, 3));

			userDto.Reviews = reviews.Select(ReviewDto.FromReview).Take(3).ToList();

			userDto.Rating = await _mediator.Send(new GetReviewsAvgRatingByReciverIdQuery(id));

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

		[HttpGet]
		[Authorize]
		[Route("[action]/{id}")]
		public async Task<IActionResult> GetOnlyUserById(string id)
		{
			ApplicationUser user = await _mediator.Send(new GetUserByIdQuery(id));

			LogLevel logLevel = LogLevel.Information;
			string message = "Succesfully get user";

			await _fileLogger.LogAsync(logLevel, message);
			await _databaseLogger.LogAsync(logLevel, message);

			return Ok(GetOnlyUserByIdDto.FromUser(user));
		}

		[HttpPost]
		[Authorize]
		[Route("[action]")]
		public async Task<IActionResult> UpdateProfilePicture([FromForm] UpdateUserProfilePictureDto updatePictureDto)
		{
			UpdateProfilePictureCommand command = new UpdateProfilePictureCommand()
			{
				ProfilePicture = updatePictureDto.ProfilePicture,
				UserId = User.Id()
			};

			await _mediator.Send(command);

			LogLevel logLevel = LogLevel.Information;
			string message = "Succesfully update user profile picture";

			await _fileLogger.LogAsync(logLevel, message);
			await _databaseLogger.LogAsync(logLevel, message);

			return Ok();
		}

		[HttpPost]
		[Authorize]
		[Route("[action]")]
		public async Task<IActionResult> Update([FromBody] UpdateUserDto updateUserDto)
		{
			UpdateApplicationUserCommand command = new UpdateApplicationUserCommand()
			{
				Id = User.Id(),
				FirstName = updateUserDto.FirstName,
				LastName = updateUserDto.LastName,
				City = updateUserDto.City,
				Country = updateUserDto.Country,
			};

			await _mediator.Send(command);

			LogLevel logLevel = LogLevel.Information;
			string message = "Succesfully update user";

			await _fileLogger.LogAsync(logLevel, message);
			await _databaseLogger.LogAsync(logLevel, message);

			return Ok();
		}
	}
}
