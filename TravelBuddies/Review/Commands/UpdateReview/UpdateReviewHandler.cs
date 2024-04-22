namespace TravelBuddies.Application.Review.Commands.UpdateReview
{
	using MediatR;
	using Microsoft.AspNetCore.Identity;
	using System.Threading;
	using System.Threading.Tasks;
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;
	using static TravelBuddies.Application.Exceptions.ExceptionMessages;

	public class UpdateReviewHandler : BaseHandler, IRequestHandler<UpdateReviewCommand, Review>
	{
		public UpdateReviewHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager)
			: base(repository, userManager, roleManager)
		{
		}

		public async Task<Review> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
		{
			Review? review = await _repository.GetByIdAsync<Review>(request.Id);

			if (review == null)
			{
				throw new ReviewNotFoundException(string.Format(ReviewNotFoundMessage, request.Id));
			}

			if (review.CreatorId != request.CreatorId)
			{
				throw new ApplicationUserNotCreatorException(string.Format(ApplicationUserNotCreatorMessage, request.CreatorId));
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
