namespace TravelBuddies.Application.Abstract
{
	using TravelBuddies.Domain.Entities;

	public interface IReviewRepository
	{
		Task CreateReviewAsync(Review review);

		void UpdateReview(Review review);

		Task<List<Review>> GetAllReviewsByReciverId(int reciverId);
		Task<Review?> GetReviewByIdAsync(int reviewId);
	}
}
