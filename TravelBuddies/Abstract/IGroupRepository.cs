namespace TravelBuddies.Application.Abstract
{
	using TravelBuddies.Domain.Entities;

	public interface IGroupRepository
	{
		Task<Group> CreateGroupAsync(Group group);
		Task<Group?> GetGroupById(int groupId);
		Task<Group?> GetGroupByPostId(int groupId);
	}
}
