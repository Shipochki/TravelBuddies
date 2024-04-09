namespace TravelBuddies.Infrastructure.Repository
{
	using Microsoft.EntityFrameworkCore;
	using TravelBuddies.Application.Abstract;
	using TravelBuddies.Domain.Entities;

	public class UserRepository : IUserRepository
	{
		private readonly TravelBuddiesDbContext _context;

        public UserRepository(TravelBuddiesDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByEmail(string email)
		{
			return await _context
				.Users
				.FirstOrDefaultAsync(u => u.IsDeleted == false && u.Email == email);
		}

		public async Task<User?> GetUserByEmailIncludeRole(string email)
		{
			return await _context
				.Users
				.Include(u => u.Role)
				.FirstOrDefaultAsync(u => u.IsDeleted == false && u.Email == email);
		}

		public async Task<User?> GetUserById(int userId)
		{
			return await _context
				.Users
				.FirstOrDefaultAsync(u => u.IsDeleted == false && u.Id == userId);
		}

		public User? Login(User user, string password)
		{
			throw new NotImplementedException();
		}

		public Task Register(User user, string password)
		{
			throw new NotImplementedException();
		}

		public void Update(User user)
		{
			_context.Update(user);
		}
	}
}
