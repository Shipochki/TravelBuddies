namespace TravelBuddies.Application.UserGroup.Commands.CreateUserGroup
{
	using MediatR;

	public class CreateUserGroupCommand : UserGroupDto, IRequest<Task>
	{
	}
}
