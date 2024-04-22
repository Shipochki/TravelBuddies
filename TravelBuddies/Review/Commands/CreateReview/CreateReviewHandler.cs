namespace TravelBuddies.Application.Review.Commands.CreateReview
{
	using MediatR;
	using Microsoft.AspNetCore.Identity;
	using System.Threading;
	using System.Threading.Tasks;
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;
	using static TravelBuddies.Application.Exceptions.ExceptionMessages;

	public class CreateReviewHandler : BaseHandler, IRequestHandler<CreateReviewCommand, Review>
	{
		public CreateReviewHandler(IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager)
			: base(repository, userManager, roleManager)
		{
		}

		public async Task<Review> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
		{
			ApplicationUser? creator = await _userManager.FindByIdAsync(request.CreatorId);

			if (creator == null)
			{
				throw new ApplicationUserNotFoundException(
					string.Format(ApplicationUserNotFoundMessage, request.CreatorId));
			}

			ApplicationUser? reciver = await _userManager.FindByIdAsync(request.ReciverId);

			if (reciver == null)
			{
				throw new ApplicationUserNotFoundException(
					string.Format(ApplicationUserNotFoundMessage, request.ReciverId));
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
