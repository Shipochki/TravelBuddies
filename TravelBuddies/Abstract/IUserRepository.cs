namespace TravelBuddies.Application.Abstract
{
	using TravelBuddies.Domain.Entities;

	public interface IUserRepository
	{
		User? Login(User user, string password);

		Task Register(User user, string password);

		void Update(User user);

		Task<User?> GetUserById(int userId);

		Task<User?> GetUserByEmail(string email);

		Task<User?> GetUserByEmailIncludeRole(string email);
	}
}
