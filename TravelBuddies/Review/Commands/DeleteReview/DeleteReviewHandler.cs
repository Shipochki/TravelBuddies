namespace TravelBuddies.Application.Review.Commands.DeleteReview
{
	using MediatR;
	using System.Threading;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;

	public class DeleteReviewHandler :  BaseHandler, IRequestHandler<DeleteReviewCommand, Task>
	{
		public DeleteReviewHandler(IRepository repository) 
			: base(repository)
		{
		}

		public async Task<Task> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
		{
			Review? review = await _repository.GetByIdAsync<Review>(request.ReviewId);

			if (review == null)
			{
				throw new ArgumentNullException($"Non-extitent Review with Id {request.ReviewId}");
			}

			if(review.CreatorId != request.CreatorId)
			{
				throw new ArgumentException($"User with Id {request.CreatorId} is not creator of review");
			}

			review.IsDeleted = true;
			review.DeletedOn = DateTime.Now;
			await _repository.SaveChangesAsync();

			return Task.CompletedTask;
		}
	}
}
