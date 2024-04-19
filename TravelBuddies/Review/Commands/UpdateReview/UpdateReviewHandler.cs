namespace TravelBuddies.Application.Review.Commands.UpdateReview
{
	using MediatR;
	using System.Threading;
	using System.Threading.Tasks;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;

	public class UpdateReviewHandler : IRequestHandler<UpdateReviewCommand, Review>
	{
		private readonly IRepository _repository;

        public UpdateReviewHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Review> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
		{
			Review? review = await _repository.GetByIdAsync<Review>(request.Id);

			if (review == null)
			{
				throw new ArgumentNullException($"Non-extitent Review with Id {request.CreatorId}");
			}

			review.Text = request.Text;
			review.Rating = request.Rating;
			review.UpdatedOn = DateTime.Now;

			_repository.Update(review);
			await _repository.SaveChangesAsync();

			return await Task.FromResult(review);
		}
	}
}
