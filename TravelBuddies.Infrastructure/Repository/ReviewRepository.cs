using Microsoft.EntityFrameworkCore;
using TravelBuddies.Application.Abstract;
using TravelBuddies.Domain.Entities;

namespace TravelBuddies.Infrastructure.Repository
{
	public class ReviewRepository : IReviewRepository
	{
		private readonly TravelBuddiesDbContext _context;

        public ReviewRepository(TravelBuddiesDbContext context)
        {
            _context = context;
        }

        public async Task CreateReviewAsync(Review review)
		{
			await _context.AddAsync(review);
		}

		public async Task<List<Review>> GetAllReviewsByReciverId(int reciverId)
		{
			return await _context
				.Reviews
				.Where(r => r.IsDeleted == false && r.ReciverId == reciverId)
				.ToListAsync();
		}

		public async Task<Review?> GetReviewByIdAsync(int reviewId)
		{
			return await _context
				.Reviews
				.FirstOrDefaultAsync(r => r.IsDeleted == false && r.Id == reviewId);
		}

		public void UpdateReview(Review review)
		{
			_context.Update(review);
		}
	}
}
