namespace TravelBuddies.Application.Message.Commands.UpdateMessage
{
	using MediatR;

	public class UpdateMessageCommand : MessageDto, IRequest<Task>
	{
	}
}
