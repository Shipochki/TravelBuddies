namespace TravelBuddies.Application.Abstract
{
	using TravelBuddies.Domain.Entities;

	public interface IUserRepository
	{
		User? Login(User user, string password);

		Task<User> Register(User user, string password);

		Task<User> Edit(User user);

		Task BecomeDriver(User user, Role role);

		Task<User?> GetUserById(int userId);

		Task Delete(User user);
		Task<User?> GetUserByEmail(string email);

		Task<User?> GetUserByEmailIncludeRole(string email);
	}
}
