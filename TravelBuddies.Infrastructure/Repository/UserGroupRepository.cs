namespace TravelBuddies.Infrastructure.Repository
{
	using TravelBuddies.Application.Abstract;
	using TravelBuddies.Domain.Entities;

	public class UserGroupRepository : IUserGroupRepository
	{
		private readonly TravelBuddiesDbContext _context;

        public UserGroupRepository(TravelBuddiesDbContext context)
        {
            _context = context;
        }

        public async Task CreateUserGroupAsync(UserGroup userGroup)
		{
			await _context.AddAsync(userGroup);
		}
	}
}
