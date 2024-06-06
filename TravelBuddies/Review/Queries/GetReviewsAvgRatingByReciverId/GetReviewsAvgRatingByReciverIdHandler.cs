namespace TravelBuddies.Application.Review.Queries.GetReviewsAvgRatingByReciverId
{
	using MediatR;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.EntityFrameworkCore;
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
			List<Review> rating = await _repository
				.AllReadonly<Review>(
					r => r.IsDeleted == false
					&& r.ReciverId == request.ReciverId)
				.ToListAsync();

			double result = rating.Count > 0
				? Math.Round(rating
					.Select(r => r.Rating)
					.Average(), 2)
				: 0;

			return result;
		}
	}
}
