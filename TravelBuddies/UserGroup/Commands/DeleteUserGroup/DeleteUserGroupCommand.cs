namespace TravelBuddies.Application.UserGroup.Commands.DeleteUserGroup
{
	using MediatR;

	public class DeleteUserGroupCommand : UserGroupDto, IRequest<Task>
	{
	}
}
