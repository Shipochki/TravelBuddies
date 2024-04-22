namespace TravelBuddies.Application.Review.Commands.DeleteReview
{
	using MediatR;
	using Microsoft.AspNetCore.Identity;
	using System.Threading;
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;
	using static TravelBuddies.Application.Exceptions.ExceptionMessages;

	public class DeleteReviewHandler :  BaseHandler, IRequestHandler<DeleteReviewCommand, Task>
	{
		public DeleteReviewHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager)
			: base(repository, userManager, roleManager)
		{
		}

		public async Task<Task> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
		{
			Review? review = await _repository.GetByIdAsync<Review>(request.ReviewId);

			if (review == null)
			{
				throw new ReviewNotFoundException(string.Format(ReviewNotFoundMessage, request.ReviewId));
			}

			if(review.CreatorId != request.CreatorId)
			{
				throw new ApplicationUserNotCreatorException(string.Format(ApplicationUserNotCreatorMessage, request.CreatorId));
			}

			review.IsDeleted = true;
			review.DeletedOn = DateTime.Now;

			await _repository.SaveChangesAsync();

			return Task.CompletedTask;
		}
	}
}
