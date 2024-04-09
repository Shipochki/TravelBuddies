namespace TravelBuddies.Application.Abstract
{
	using TravelBuddies.Domain.Entities;

	public interface IRoleRepository
	{
		Task<Role?> GetRoleByNameAsync(string name);

		Task<Role?> GetRoleByIdAsync(int roleId);
	}
}
