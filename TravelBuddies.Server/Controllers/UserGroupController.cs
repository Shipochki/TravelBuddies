namespace TravelBuddies.Presentation.Controllers
{
	using MediatR;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Cors;
	using Microsoft.AspNetCore.Mvc;
	using TravelBuddies.Domain.Common;
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.UserGroup.Commands.CreateUserGroup;
	using TravelBuddies.Application.UserGroup.Commands.DeleteUserGroup;
	using TravelBuddies.Application.UserGroup.Commands.RemoveUserGroup;
	using TravelBuddies.Domain.Enums;
	using TravelBuddies.Presentation.Configurations;
	using TravelBuddies.Presentation.DTOs.UserGroup;

	[EnableCors(ApplicationCorses.AllowOrigin)]
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class UserGroupController : BaseController
	{
		public UserGroupController(IMediator mediator)
			: base(mediator)
		{
		}

		[HttpPost]
		[Route("[action]")]
		public async Task<IActionResult> JoinGroup(int groupId)
		{
			LogLevel logLevel = LogLevel.Error;

			try
			{
				CreateUserGroupCommand command = new CreateUserGroupCommand()
				{
					GroupId = groupId,
					UserId = User.Id(),
				};

				await _mediator.Send(command);

				logLevel = LogLevel.Information;
				string message = "Succesfully joined in group";

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
			catch (GroupNotFoundException m)
			{
				await _fileLogger.LogAsync(logLevel, m.Message);
				await _databaseLogger.LogAsync(logLevel, m.Message);

				return NotFound(m.Message);
			}
			catch (ApplicationUserAllreadyInGroupException m)
			{
				await _fileLogger.LogAsync(logLevel, m.Message);
				await _databaseLogger.LogAsync(logLevel, m.Message);

				return BadRequest(m.Message);
			}
			catch (PostNotFoundException m)
			{
				await _fileLogger.LogAsync(logLevel, m.Message);
				await _databaseLogger.LogAsync(logLevel, m.Message);

				return NotFound(m.Message);
			}
			catch (NotAvailableSeatsInPostException m)
			{
				await _fileLogger.LogAsync(logLevel, m.Message);
				await _databaseLogger.LogAsync(logLevel, m.Message);

				return BadRequest(m.Message);
			}
		}

		[HttpPost]
		[Route("[action]")]
		public async Task<IActionResult> LeaveGroup(int groupId)
		{
			LogLevel logLevel = LogLevel.Error;

			try
			{
				DeleteUserGroupCommand command = new DeleteUserGroupCommand()
				{
					GroupId = groupId,
					UserId = User.Id(),
				};

				await _mediator.Send(command);

				logLevel = LogLevel.Information;
				string message = "Succesfully leave group";

				await _fileLogger.LogAsync(logLevel, message);
				await _databaseLogger.LogAsync(logLevel, message);

				return Ok(message);
			}
			catch (ApplicationUserNotInGroupException m)
			{
				await _fileLogger.LogAsync(logLevel, m.Message);
				await _databaseLogger.LogAsync(logLevel, m.Message);

				return BadRequest(m.Message);
			}
		}

		[HttpPost]
		[Route("[action]")]
		[Authorize(Policy = ApplicationPolicies.DriverAndAdmin)]
		public async Task<IActionResult> RemoveUserFromGroup([FromBody] RemoveUserGroupDto removeUserGroupDto)
		{
			LogLevel logLevel = LogLevel.Error;

			try
			{
				RemoveUserGroupCommand command = new RemoveUserGroupCommand()
				{
					GroupId = removeUserGroupDto.GroupId,
					UserIdForRemove = removeUserGroupDto.UserId,
					OwnerId = User.Id()
				};

				await _mediator.Send(command);

				logLevel = LogLevel.Information;
				string message = "Succesfully leave group";

				await _fileLogger.LogAsync(logLevel, message);
				await _databaseLogger.LogAsync(logLevel, message);

				return Ok(message);
			}
			catch (ApplicationUserNotInGroupException m)
			{
				await _fileLogger.LogAsync(logLevel, m.Message);
				await _databaseLogger.LogAsync(logLevel, m.Message);

				return BadRequest(m.Message);
			}
			catch (ApplicationUserNotFoundException m)
			{
				await _fileLogger.LogAsync(logLevel, m.Message);
				await _databaseLogger.LogAsync(logLevel, m.Message);

				return NotFound(m.Message);
			}
			catch (GroupNotFoundException m)
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
