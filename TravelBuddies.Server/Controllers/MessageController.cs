namespace TravelBuddies.Presentation.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;
    using TravelBuddies.Domain.Common;
    using TravelBuddies.Application.Message.Commands.CreateMessage;
    using TravelBuddies.Application.Message.Commands.DeleteMessage;
    using TravelBuddies.Application.Message.Commands.UpdateMessage;
    using TravelBuddies.Application.Message.Queries.GetMessagesByGroupId;
    using TravelBuddies.Domain.Entities;
    using TravelBuddies.Domain.Enums;
    using TravelBuddies.Presentation.DTOs.Message;
    using TravelBuddies.Presentation.Filters;
    using TravelBuddies.Presentation.Extensions;

    [EnableCors(ApplicationCorses.AllowOrigin)]
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class MessageController : BaseController
	{
		public MessageController(IMediator mediator)
			: base(mediator)
		{
		}

		[HttpPost]
		[Route("[action]")]
		public async Task<IActionResult> Create([FromBody] CreateMessageDto createMessageDto)
		{
			CreateMessageCommand command = new CreateMessageCommand()
			{
				Text = createMessageDto.Text,
				GroupId = createMessageDto.GroupId,
				CreatorId = User.Id(),
			};

			await _mediator.Send(command);

			LogLevel logLevel = LogLevel.Information;
			string message = "Succesfully created message";

			await _fileLogger.LogAsync(logLevel, message);
			await _databaseLogger.LogAsync(logLevel, message);

			return Created();
		}

		[HttpPost]
		[Route("[action]")]
		public async Task<IActionResult> Update([FromBody] UpdateMessageDto updateMessageDto)
		{
			UpdateMessageCommand command = new UpdateMessageCommand()
			{
				Id = updateMessageDto.Id,
				Text = updateMessageDto.Text,
				GroupId = updateMessageDto.GroupId,
				CreatorId = User.Id()
			};

			await _mediator.Send(command);

			LogLevel logLevel = LogLevel.Information;
			string message = "Succesfully update message";

			await _fileLogger.LogAsync(logLevel, message);
			await _databaseLogger.LogAsync(logLevel, message);

			return Ok(message);
		}

		[HttpPost]
		[Route("[action]/{messageId}")]
		public async Task<IActionResult> Delete(int messageId)
		{
			await _mediator.Send(new DeleteMessageCommand(messageId, User.Id()));

			LogLevel logLevel = LogLevel.Information;
			string message = "Succesful delete message";

			await _fileLogger.LogAsync(logLevel, message);
			await _databaseLogger.LogAsync(logLevel, message);

			return Ok(message);
		}

		[HttpGet]
		[Route("[action]/{groupId}")]
		public async Task<IActionResult> GetMessagesByGroupId(int groupId)
		{
			List<Message> messages = await _mediator.Send(new GetMessagesByGroupIdQuery(groupId, User.Id()));

			LogLevel logLevel = LogLevel.Information;
			string message = "Succesfully get messages by group id";

			await _fileLogger.LogAsync(logLevel, message);
			await _databaseLogger.LogAsync(logLevel, message);

			return Ok(messages.Select(GetMessagesByGroupIdDto.FromMessage));
		}
	}
}
