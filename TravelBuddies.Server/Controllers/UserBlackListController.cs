namespace TravelBuddies.Presentation.Controllers
{
	using MediatR;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Cors;
	using Microsoft.AspNetCore.Mvc;
	using TravelBuddies.Application.UserBlackList.Command.CreateUserBlackList;
	using TravelBuddies.Application.UserGroup.Commands.RemoveUserGroup;
	using TravelBuddies.Domain.Common;
	using TravelBuddies.Domain.Enums;
	using TravelBuddies.Presentation.DTOs.UserBlackList;
	using TravelBuddies.Presentation.Extensions;

	[EnableCors(ApplicationCorses.AllowOrigin)]
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class UserBlackListController : BaseController
	{
		public UserBlackListController(IMediator mediator)
			: base(mediator)
		{
		}

		[HttpPost]
		[Route("[action]")]
		[Authorize(Policy = ApplicationPolicies.DriverAndAdmin)]
		public async Task<IActionResult> Create([FromBody] CreateUserBlackListDto userBlackListFromKeys)
		{
			CreateUserBlackListCommand command = new CreateUserBlackListCommand()
			{
				GroupId = userBlackListFromKeys.GroupId,
				UserId = userBlackListFromKeys.UserId,
				OwnerId = User.Id()
			};

			await _mediator.Send(command);

			RemoveUserGroupCommand removeCommand = new RemoveUserGroupCommand()
			{
				GroupId = userBlackListFromKeys.GroupId,
				UserIdForRemove = userBlackListFromKeys.UserId,
				OwnerId = User.Id()
			};

			await _mediator.Send(removeCommand);

			LogLevel logLevel = LogLevel.Information;
			string message = "Succesfully created user black lists";

			await _fileLogger.LogAsync(logLevel, message);
			await _databaseLogger.LogAsync(logLevel, message);

			return Ok();
		}
	}
}
