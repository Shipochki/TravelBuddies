namespace TravelBuddies.Application.Group.Commands.CreateGroup
{
	using MediatR;
	using TravelBuddies.Domain.Entities;

	public class CreateGroupCommand : GroupDto, IRequest<Group>
	{
	}
}
