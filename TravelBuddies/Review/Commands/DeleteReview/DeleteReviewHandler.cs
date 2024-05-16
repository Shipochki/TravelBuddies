namespace TravelBuddies.Application.Review.Commands.DeleteReview
{
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using System.Threading;
    using TravelBuddies.Domain.Common;
    using TravelBuddies.Domain.Entities;
    using static TravelBuddies.Application.Common.Exceptions.Messages.ExceptionMessages;
    using TravelBuddies.Application.Common.Interfaces.Repository;
	using TravelBuddies.Application.Common.Exceptions.NotFound;
	using TravelBuddies.Application.Common.Exceptions.Forbidden;

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
				throw new ReviewNotFoundException(
					string.Format(ReviewNotFoundMessage, request.ReviewId));
			}

			ApplicationUser? user = await _userManager.FindByIdAsync(request.CreatorId);

			if (user == null)
			{
				throw new ApplicationUserNotFoundException(
					string.Format(ApplicationUserNotFoundMessage, request.CreatorId));
			}

			if (review.CreatorId != request.CreatorId 
				&& !await _userManager.IsInRoleAsync(user, ApplicationRoles.Admin))
			{
				throw new ApplicationUserNotCreatorException(
					string.Format(ApplicationUserNotCreatorMessage, request.CreatorId));
			}

			review.IsDeleted = true;
			review.DeletedOn = DateTime.Now;

			await _repository.SaveChangesAsync();

			return Task.CompletedTask;
		}
	}
}
