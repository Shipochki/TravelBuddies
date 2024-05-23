namespace TravelBuddies.Presentation.Controllers
{
	using MediatR;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Cors;
	using Microsoft.AspNetCore.Mvc;
	using TravelBuddies.Application.UserBlackList.Command.CreateUserBlackList;
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
		public async Task<IActionResult> BanUserFromGroup([FromBody] CreateUserBlackListDto createUserBlackListDto)
		{
			CreateUserBlackListCommand command = new CreateUserBlackListCommand()
			{
				GroupId = createUserBlackListDto.GroupId,
				UserId = createUserBlackListDto.UserId,
				OwnerId = User.Id()
			};

			await _mediator.Send(command);

			LogLevel logLevel = LogLevel.Information;
			string message = "Succesfully created user black lists";

			await _fileLogger.LogAsync(logLevel, message);
			await _databaseLogger.LogAsync(logLevel, message);

			return Ok(message);
		}
	}
}
