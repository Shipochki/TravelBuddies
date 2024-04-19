namespace TravelBuddies.Application.Review.Commands.DeleteReview
{
	using MediatR;
	using System.Threading;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;

	public class DeleteReviewHandler : IRequestHandler<DeleteReviewCommand, Task>
	{
		private readonly IRepository _repository;

        public DeleteReviewHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Task> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
		{
			Review? review = await _repository.GetByIdAsync<Review>(request.Id);

			if (review == null)
			{
				throw new ArgumentNullException($"Non-extitent Review with Id {request.Id}");
			}

			_repository.Delete(review);
			await _repository.SaveChangesAsync();

			return Task.CompletedTask;
		}
	}
}
