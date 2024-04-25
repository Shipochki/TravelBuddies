namespace TravelBuddies.Presentation.Controllers
{
	using MediatR;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Cors;
	using Microsoft.AspNetCore.Mvc;
	using TravelBuddies.Application.Constants;
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Group.Commands.CreateGroup;
	using TravelBuddies.Application.Post.Commands.CreatePost;
	using TravelBuddies.Application.Post.Commands.DeletePost;
	using TravelBuddies.Application.Post.Commands.UpdatePost;
	using TravelBuddies.Application.Post.Commands.UpdatePostGroup;
	using TravelBuddies.Application.Post.Queries.GetPostsBySearch;
	using TravelBuddies.Application.UserGroup.Commands.CreateUserGroup;
	using TravelBuddies.Domain.Entities;
	using TravelBuddies.Domain.Enums;
	using TravelBuddies.Presentation.Configurations;
	using TravelBuddies.Presentation.DTOs.Post;

	[EnableCors(ApplicationCorses.AllowOrigin)]
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class PostController : BaseController
	{
		public PostController(IMediator mediator)
			: base(mediator)
		{
		}

		[HttpGet]
		[Route("[action]")]
		public async Task<IActionResult> GetAllPostBySearch([FromBody] PostSearchDto postSearchDto)
		{
			LogLevel logLevel;

			GetPostBySearchQuery command = new GetPostBySearchQuery()
			{
				FromDate = postSearchDto.FromDate,
				ToDate = postSearchDto.ToDate,
				FromDestinationCityId = postSearchDto.FromDestinationCityId,
				ToDestinationCityId = postSearchDto.ToDestinationCityId,
				PriceMin = postSearchDto.PriceMin,
				PriceMax = postSearchDto.PriceMax,
				Baggage = postSearchDto.Baggage,
				Pets = postSearchDto.Pets,
			};

			List<Post> posts = await _mediator.Send(command);

			logLevel = LogLevel.Information;
			string message = "Succesfully get all posts by search";

			await _fileLogger.LogAsync(logLevel, message);
			await _databaseLogger.LogAsync(logLevel, message);

			return Ok(posts.Select(GetAllPostsBySearchDto.FromPost));
		}

		[HttpPost]
		[Route("[action]")]
		[Authorize(Policy = ApplicationPolicies.OnlyDriver)]
		public async Task<IActionResult> Create([FromBody] CreatePostDto createPostDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			LogLevel logLevel = LogLevel.Error;

			try
			{
				CreatePostCommand postCommand = new CreatePostCommand()
				{
					FromDestinationCityId = createPostDto.FromDestinationCityId,
					ToDestinationCityId = createPostDto.ToDestinationCityId,
					Description = createPostDto.Description,
					PricePerSeat = createPostDto.PricePerSeat,
					FreeSeats = createPostDto.FreeSeats,
					Baggage = createPostDto.Baggage,
					Pets = createPostDto.Pets,
					DateAndTime = createPostDto.DateAndTime,
					PaymentType = createPostDto.PaymentType,
					CreatorId = User.Id()
				};

				Post post = await _mediator.Send(postCommand);

				CreateGroupCommand groupCommand = new CreateGroupCommand()
				{
					CreatorId = post.CreatorId,
					PostId = post.Id,
				};

				Group group = await _mediator.Send(groupCommand);

				UpdatePostGroupCommand updatePostGroupCommand = new UpdatePostGroupCommand(post.Id, group.Id);

				await _mediator.Send(updatePostGroupCommand);

				CreateUserGroupCommand userGroupCommand = new CreateUserGroupCommand()
				{
					GroupId = group.Id,
					UserId = group.CreatorId,
				};

				await _mediator.Send(userGroupCommand);

				logLevel = LogLevel.Information;
				string message = "Succesfully created post with group and creator joined group";

				await _fileLogger.LogAsync(logLevel, message);
				await _databaseLogger.LogAsync(logLevel, message);

				return Created();
			}
			catch (ApplicationUserNotFoundException m)
			{
				await _fileLogger.LogAsync(logLevel, m.Message);
				await _databaseLogger.LogAsync(logLevel, m.Message);

				return NotFound(m.Message);
			}
			catch (CityNotFoundException m)
			{
				await _fileLogger.LogAsync(logLevel, m.Message);
				await _databaseLogger.LogAsync(logLevel, m.Message);

				return NotFound(m.Message);
			}
			catch (PostNotFoundException m)
			{
				await _fileLogger.LogAsync(logLevel, m.Message);
				await _databaseLogger.LogAsync(logLevel, m.Message);

				return NotFound(m.Message);
			}
		}

		[HttpPost]
		[Route("[action]")]
		[Authorize(Policy = ApplicationPolicies.DriverAndAdmin)]
		public async Task<IActionResult> Delete(int postId)
		{
			LogLevel logLevel = LogLevel.Error;

			try
			{
				await _mediator.Send(new DeletePostCommand(postId, User.Id()));

				logLevel = LogLevel.Information;
				string message = "Succesfully deleted post";

				await _fileLogger.LogAsync(logLevel, message);
				await _databaseLogger.LogAsync(logLevel, message);

				return Ok(message);
			}
			catch (PostNotFoundException m)
			{
				await _fileLogger.LogAsync(logLevel, m.Message);
				await _databaseLogger.LogAsync(logLevel, m.Message);

				return NotFound(m.Message);
			}
			catch (ApplicationUserNotFoundException m)
			{
				await _fileLogger.LogAsync(logLevel, m.Message);
				await _databaseLogger.LogAsync(logLevel, m.Message);

				return NotFound(m.Message);
			}
			catch (ApplicationUserNotCreatorException m)
			{
				await _fileLogger.LogAsync(logLevel, m.Message);
				await _databaseLogger.LogAsync(logLevel, m.Message);

				return Forbid(m.Message);
			}
		}

		[HttpPost]
		[Route("[action]")]
		[Authorize(Policy =ApplicationPolicies.OnlyDriver)]
		public async Task<IActionResult> Update([FromBody] UpdatePostDto updatePostDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			LogLevel logLevel = LogLevel.Error;

			try
			{
				UpdatePostCommand command = new UpdatePostCommand()
				{
					Id = updatePostDto.Id,
					FromDestinationCityId = updatePostDto.FromDestinationCityId,
					ToDestinationCityId = updatePostDto.ToDestinationCityId,
					Description = updatePostDto.Description,
					PricePerSeat = updatePostDto.PricePerSeat,
					FreeSeats = updatePostDto.FreeSeats,
					Baggage = updatePostDto.Baggage,
					Pets = updatePostDto.Pets,
					DateAndTime = updatePostDto.DateAndTime,
					PaymentType = updatePostDto.PaymentType,
					CreatorId = User.Id()
				};

				await _mediator.Send(command);

				logLevel = LogLevel.Information;
				string message = "Succesfully updated post";

				await _fileLogger.LogAsync(logLevel, message);
				await _databaseLogger.LogAsync(logLevel, message);

				return Ok(message);
			}
			catch (PostNotFoundException m)
			{
				await _fileLogger.LogAsync(logLevel, m.Message);
				await _databaseLogger.LogAsync(logLevel, m.Message);

				return NotFound(m.Message);
			}
			catch (ApplicationUserNotFoundException m)
			{
				await _fileLogger.LogAsync(logLevel, m.Message);
				await _databaseLogger.LogAsync(logLevel, m.Message);

				return NotFound(m.Message);
			}
			catch (ApplicationUserNotCreatorException m)
			{
				await _fileLogger.LogAsync(logLevel, m.Message);
				await _databaseLogger.LogAsync(logLevel, m.Message);

				return Forbid(m.Message);
			}
		}
	}
}
