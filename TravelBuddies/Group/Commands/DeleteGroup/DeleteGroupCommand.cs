namespace TravelBuddies.Application.Group.Commands.DeleteGroup
{
	using MediatR;

	public class DeleteGroupCommand : GroupDto, IRequest<Task>
	{
	}
}
