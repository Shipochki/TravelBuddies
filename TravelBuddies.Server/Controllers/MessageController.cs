namespace TravelBuddies.Presentation.Controllers
{
	using MediatR;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Cors;
	using Microsoft.AspNetCore.Mvc;
	using TravelBuddies.Application.Constants;
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Message.Commands.CreateMessage;
	using TravelBuddies.Application.Message.Commands.DeleteMessage;
	using TravelBuddies.Application.Message.Commands.UpdateMessage;
	using TravelBuddies.Application.Message.Queries.GetMessagesByGroupId;
	using TravelBuddies.Domain.Entities;
	using TravelBuddies.Domain.Enums;
	using TravelBuddies.Presentation.Configurations;
	using TravelBuddies.Presentation.DTOs.Message;

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
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			LogLevel logLevel = LogLevel.Error;

			try
			{
				CreateMessageCommand command = new CreateMessageCommand()
				{
					Text = createMessageDto.Text,
					GroupId = createMessageDto.GroupId,
					CreatorId = User.Id(),
				};

				await _mediator.Send(command);

				logLevel = LogLevel.Information;
				string message = "Succesfully created message";

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
		}

		[HttpPost]
		[Route("[action]")]
		public async Task<IActionResult> Update([FromBody] UpdateMessageDto updateMessageDto)
		{
			if(!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			LogLevel logLevel = LogLevel.Error;

			try
			{
				UpdateMessageCommand command = new UpdateMessageCommand()
				{
					Id = updateMessageDto.Id,
					Text = updateMessageDto.Text,
					GroupId = updateMessageDto.GroupId,
					CreatorId = User.Id()
				};

				await _mediator.Send(command);

				logLevel = LogLevel.Information;
				string message = "Succesfully update message";

				await _fileLogger.LogAsync(logLevel, message);
				await _databaseLogger.LogAsync(logLevel, message);

				return Ok(message);
			}
			catch (MessageNotFoundException m)
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
		public async Task<IActionResult> Delete(int messageId)
		{
			LogLevel logLevel = LogLevel.Error;

			try
			{
				await _mediator.Send(new DeleteMessageCommand(messageId, User.Id()));

				logLevel = LogLevel.Information;
				string message = "Succesful delete message";

				await _fileLogger.LogAsync(logLevel, message);
				await _databaseLogger.LogAsync(logLevel, message);

				return Ok(message);
			}
			catch (MessageNotFoundException m)
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

		[HttpGet]
		[Route("[action]")]
		public async Task<IActionResult> GetMessagesByGroupId(int groupId)
		{
			LogLevel logLevel = LogLevel.Error;

			try
			{
				List<Message> messages = await _mediator.Send(new GetMessagesByGroupIdQuery(groupId, User.Id()));

				logLevel = LogLevel.Information;
				string message = "Succesfully get messages by group id";

				await _fileLogger.LogAsync(logLevel, message);
				await _databaseLogger.LogAsync(logLevel, message);

				return Ok(messages.Select(GetMessagesByGroupIdDto.FromMessage));
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
			catch(ApplicationUserNotInGroupException m)
			{
				await _fileLogger.LogAsync(logLevel, m.Message);
				await _databaseLogger.LogAsync(logLevel, m.Message);

				return Forbid(m.Message);
			}
		}
	}
}
