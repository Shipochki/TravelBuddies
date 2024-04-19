namespace TravelBuddies.Application.Review.Commands.CreateReview
{
	using MediatR;
	using System.Threading;
	using System.Threading.Tasks;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;

	public class CreateReviewHandler : IRequestHandler<CreateReviewCommand, Review>
	{
		private readonly IRepository _repository;

        public CreateReviewHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Review> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
		{
			ApplicationUser? creator = await _repository.GetByIdAsync<ApplicationUser>(request.CreatorId);

			if (creator == null)
			{
				throw new ArgumentNullException($"Non-extitent User with Id {request.CreatorId}");
			}

			ApplicationUser? reciver = await _repository.GetByIdAsync<ApplicationUser>(request.ReciverId);

			if (reciver == null)
			{
				throw new ArgumentNullException($"Non-extitent User with Id {request.ReciverId}");
			}

			Review review = new Review()
			{
				Creator = creator,
				CreatorId = creator.Id,
				Text = request.Text,
				Rating = request.Rating,
				Reciver = reciver,
				ReciverId = reciver.Id,
				CreatedOn = DateTime.Now,
			};

			await _repository.AddAsync(review);
			await _repository.SaveChangesAsync();

			return await Task.FromResult(review);
		}
	}
}
