namespace TravelBuddies.Application.Abstract
{
	using TravelBuddies.Domain.Entities;

	public interface IUserGroupRepository
	{
		Task CreateUserGroupAsync(UserGroup userGroup);
	}
}
