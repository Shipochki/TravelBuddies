namespace TravelBuddies.Application.Abstract
{
	using TravelBuddies.Domain.Entities;

	public interface IGroupRepository
	{
		Task CreateGroupAsync(Group group);
		Task<Group?> GetGroupById(int groupId);
		Task<Group?> GetGroupByPostId(int postId);
	}
}
