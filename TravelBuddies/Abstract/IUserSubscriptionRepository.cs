namespace TravelBuddies.Application.Abstract
{
	using TravelBuddies.Domain.Entities;

	public interface IUserSubscriptionRepository
	{
		Task AddSubcription(UserSubscription subcription);

		Task RemoveSubcription(UserSubscription subscription);

		Task<UserSubscription?> GetSubcriptionByUserId(int userId);
	}
}
