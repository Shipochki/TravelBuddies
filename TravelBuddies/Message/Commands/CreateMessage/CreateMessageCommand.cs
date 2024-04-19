namespace TravelBuddies.Application.Message.Commands.CreateMessage
{
	using MediatR;
	using TravelBuddies.Domain.Entities;

	public class CreateMessageCommand : MessageDto, IRequest<Message>
	{
	}
}
