namespace TravelBuddies.Application.Abstract
{
	using TravelBuddies.Domain.Entities;

	public interface IReviewRepository
	{
		Task<Review> CreateReviewAsync(Review review);

		Task<Review> EditReviewAsync(Review review);

		Task DeleteReviewAsync(Review review);

		Task<List<Review>> GetAllReviewsByReciverId(int reciverId);
		Task<Review?> GetReviewByIdAsync(int reviewId);
	}
}
