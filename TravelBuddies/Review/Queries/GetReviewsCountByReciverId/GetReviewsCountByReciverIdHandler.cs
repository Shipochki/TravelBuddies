namespace TravelBuddies.Application.Review.Queries.GetReviewsCountByReciverId
{
	using MediatR;
	using Microsoft.AspNetCore.Identity;
	using System.Threading;
	using System.Threading.Tasks;
	using TravelBuddies.Application.Common.Interfaces.Repository;
	using TravelBuddies.Domain.Entities;

	public class GetReviewsCountByReciverIdHandler : BaseHandler, IRequestHandler<GetReviewsCountByReciverIdQuery, int>
	{
		public GetReviewsCountByReciverIdHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager)
			: base(repository, userManager, roleManager)
		{
		}

		public async Task<int> Handle(GetReviewsCountByReciverIdQuery request, CancellationToken cancellationToken)
		{
			return _repository
				.All<Review>(r => r.IsDeleted == false && r.ReciverId == request.ReciverId)
				.Count();
		}
	}
}
