namespace TravelBuddies.Presentation.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;
    using TravelBuddies.Domain.Common;
    using TravelBuddies.Application.Group.Commands.CreateGroup;
    using TravelBuddies.Application.Post.Commands.CreatePost;
    using TravelBuddies.Application.Post.Commands.DeletePost;
    using TravelBuddies.Application.Post.Commands.UpdatePost;
    using TravelBuddies.Application.Post.Commands.UpdatePostGroup;
    using TravelBuddies.Application.Post.Queries.GetPostsBySearch;
    using TravelBuddies.Application.UserGroup.Commands.CreateUserGroup;
    using TravelBuddies.Domain.Entities;
    using TravelBuddies.Domain.Enums;
    using TravelBuddies.Presentation.DTOs.Post;
    using TravelBuddies.Presentation.Filters;
    using System.Text.Json;
    using TravelBuddies.Application.Post.Queries.GetPostsByOwnerId;
    using TravelBuddies.Presentation.Extensions;
	using TravelBuddies.Application.Post.Commands.CompletePost;

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
		public async Task<IActionResult> AllPostBySearch(
			[FromQuery] int fromDestinationCityId,
			[FromQuery] int toDestinationCityId,
			[FromQuery] decimal? priceMin,
			[FromQuery] decimal? priceMax,
			[FromQuery] string? fromDate,
			[FromQuery] string? toDate,
			[FromQuery] bool? baggage,
			[FromQuery] bool? pets)
		{
			LogLevel logLevel;

			GetPostBySearchQuery command = new GetPostBySearchQuery()
			{
				FromDate = fromDate,
				ToDate = toDate,
				FromDestinationCityId = fromDestinationCityId,
				ToDestinationCityId = toDestinationCityId,
				PriceMin = priceMin,
				PriceMax = priceMax,
				Baggage = baggage,
				Pets = pets,
			};

			List<Post> posts = await _mediator.Send(command);

			logLevel = LogLevel.Information;
			string message = "Succesfully get all posts by search";

			await _fileLogger.LogAsync(logLevel, message);
			await _databaseLogger.LogAsync(logLevel, message);

			var result = posts.Select(GetAllPostsBySearchDto.FromPost).ToList();
			var response = JsonSerializer.Serialize(result);

			return Ok(response);
		}

		[HttpPost]
		[Route("[action]")]
		[Authorize(Policy = ApplicationPolicies.OnlyDriver)]
		public async Task<IActionResult> Create([FromBody] CreatePostDto createPostDto)
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
				DateAndTime = $"{DateTime.Parse(createPostDto.Date).ToString("yyyy.MM.dd")} {createPostDto.Time}",
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

			LogLevel logLevel = LogLevel.Information;
			string message = "Succesfully created post with group and creator joined group";

			await _fileLogger.LogAsync(logLevel, message);
			await _databaseLogger.LogAsync(logLevel, message);

			return Created();
		}

		[HttpPost]
		[Route("[action]/{postId}")]
		[Authorize(Policy = ApplicationPolicies.DriverAndAdmin)]
		public async Task<IActionResult> Delete(int postId)
		{
			await _mediator.Send(new DeletePostCommand(postId, User.Id()));

			LogLevel logLevel = LogLevel.Information;
			string message = "Succesfully deleted post";

			await _fileLogger.LogAsync(logLevel, message);
			await _databaseLogger.LogAsync(logLevel, message);

			return Ok();
		}

		[HttpPost]
		[Route("[action]")]
		[Authorize(Policy = ApplicationPolicies.OnlyDriver)]
		public async Task<IActionResult> Update([FromBody] UpdatePostDto updatePostDto)
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

			LogLevel logLevel = LogLevel.Information;
			string message = "Succesfully updated post";

			await _fileLogger.LogAsync(logLevel, message);
			await _databaseLogger.LogAsync(logLevel, message);

			return Ok();
		}

		[HttpPost]
		[Route("[action]/{postId}")]
		[Authorize(Policy = ApplicationPolicies.OnlyDriver)]
		public async Task<IActionResult> Complete(int postId)
		{
			await _mediator.Send(new CompletePostCommand(postId, User.Id());

			LogLevel logLevel = LogLevel.Information;
			string message = "Succesfully complete post";

			await _fileLogger.LogAsync(logLevel, message);
			await _databaseLogger.LogAsync(logLevel, message);\

			return Ok();
		} 

		[HttpGet]
		[Route("[action]/{ownerId}")]
		[Authorize(Policy = ApplicationPolicies.OnlyDriver)]
		public async Task<IActionResult> GetPostsByOwnerId(string ownerId)
		{
			List<Post> posts = await _mediator.Send(new GetPostsByOwnerIdQuery(ownerId));

			LogLevel logLevel = LogLevel.Information;
			string message = "Succesfully get posts by owner id";

			await _fileLogger.LogAsync(logLevel, message);
			await _databaseLogger.LogAsync(logLevel, message);

			return Ok(posts.Select(PostDto.FromPost));
		}
	}
}
