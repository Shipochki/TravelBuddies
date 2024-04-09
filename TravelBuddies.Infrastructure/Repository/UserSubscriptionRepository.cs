namespace TravelBuddies.Infrastructure.Repository
{
	using Microsoft.EntityFrameworkCore;
	using TravelBuddies.Application.Abstract;
	using TravelBuddies.Domain.Entities;

	public class UserSubscriptionRepository : IUserSubscriptionRepository
	{
		private readonly TravelBuddiesDbContext _context;

        public UserSubscriptionRepository(TravelBuddiesDbContext context)
        {
            _context = context;
        }

        public async Task AddSubcription(UserSubscription subcription)
		{
			await _context.AddAsync(subcription);
		}

		public async Task<UserSubscription?> GetSubcriptionByUserId(int userId)
		{
			return await _context
				.UsersSubscriptions
				.FirstOrDefaultAsync(s => s.UserId == userId);
		}

		public void RemoveSubcription(UserSubscription subscription)
		{
			_context.Remove(subscription);
		}
	}
}
