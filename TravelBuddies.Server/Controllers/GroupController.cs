namespace TravelBuddies.Presentation.Controllers
{
	using MediatR;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using TravelBuddies.Application.Exceptions;
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
			LogLevel logLevel = LogLevel.Error;

			try
			{
				List<Group> groups = await _mediator.Send(new GetUserGroupsByUserIdQuery(User.Id()));

				logLevel = LogLevel.Information;
				string message = "Succesfull get groups";

				await _fileLogger.LogAsync(logLevel, message);
				await _databaseLogger.LogAsync(logLevel, message);

				return Ok(groups.Select(GetAllGroupByUserIdDto.FromGroup));
			}
			catch (ApplicationUserNotFoundException m)
			{
				await _fileLogger.LogAsync(logLevel, m.Message);
				await _databaseLogger.LogAsync(logLevel, m.Message);

				return NotFound(m.Message);
			}
		}
	}
}
