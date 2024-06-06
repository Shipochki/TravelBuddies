namespace TravelBuddies.Application.Review.Queries.GetReviewsAvgRatingByReciverId
{
	using MediatR;
	using Microsoft.AspNetCore.Identity;
	using System.Threading;
	using System.Threading.Tasks;
	using TravelBuddies.Application.Common.Interfaces.Repository;
	using TravelBuddies.Domain.Entities;

	public class GetReviewsAvgRatingByReciverIdHandler : BaseHandler, IRequestHandler<GetReviewsAvgRatingByReciverIdQuery, double>
	{
		public GetReviewsAvgRatingByReciverIdHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager)
			: base(repository, userManager, roleManager)
		{
		}

		public async Task<double> Handle(GetReviewsAvgRatingByReciverIdQuery request, CancellationToken cancellationToken)
		{
			double rating = _repository
				.AllReadonly<Review>(
					r => r.IsDeleted == false 
					&& r.ReciverId == request.ReciverId)
				.Average(r => r.Rating);

			return await Task.FromResult(Math.Round(rating, 2));
		}
	}
}
