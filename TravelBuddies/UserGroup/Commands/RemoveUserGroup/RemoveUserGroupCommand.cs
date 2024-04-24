namespace TravelBuddies.Application.UserGroup.Commands.RemoveUserGroup
{
	using MediatR;

	public class RemoveUserGroupCommand : IRequest<Task>
	{
		public required string OwnerId { get; set; }

		public required string UserIdForRemove { get; set; }

		public int GroupId { get; set; }
	}
}
