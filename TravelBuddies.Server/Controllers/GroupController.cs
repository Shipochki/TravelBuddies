namespace TravelBuddies.Presentation.Controllers
{
	using MediatR;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using TravelBuddies.Application.Group.Queries.GetGroupById;
	using TravelBuddies.Application.Group.Queries.GetUserGroupsByUserId;
	using TravelBuddies.Domain.Entities;
	using TravelBuddies.Domain.Enums;
	using TravelBuddies.Presentation.Configurations;
	using TravelBuddies.Presentation.DTOs.Group;

	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class GroupController : BaseController
	{
		public GroupController(IMediator mediator)
			: base(mediator)
		{
		}

		[HttpGet]
		[Route("[action]")]
		public async Task<IActionResult> GetUserGroupsByUserId()
		{
			List<Group> groups = await _mediator.Send(new GetUserGroupsByUserIdQuery(User.Id()));

			LogLevel logLevel = LogLevel.Information;
			string message = "Succesfull get groups";

			await _fileLogger.LogAsync(logLevel, message);
			await _databaseLogger.LogAsync(logLevel, message);

			return Ok(groups.Select(GetAllGroupByUserIdDto.FromGroup));
		}

		[HttpGet]
		[Route("[action]/{id}")]
		public async Task<IActionResult> GetGroupById(int id)
		{
			Group group = await _mediator.Send(new GetGroupByIdQuery(id, User.Id()));

			LogLevel logLevel = LogLevel.Information;
			string message = "Succesfull get group";

			await _fileLogger.LogAsync(logLevel, message);
			await _databaseLogger.LogAsync(logLevel, message);

			return Ok(GetGroupByIdDto.FromGroup(group));
		}
	}
}
