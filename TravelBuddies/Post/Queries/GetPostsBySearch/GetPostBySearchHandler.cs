namespace TravelBuddies.Application.Post.Queries.GetPostsBySearch
{
	using MediatR;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.EntityFrameworkCore;
	using System.Threading;
	using System.Threading.Tasks;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;

	public class GetPostBySearchHandler : BaseHandler, IRequestHandler<GetPostBySearchQuery, IEnumerable<Post>>
	{
		private DateTime _minDate;

		public GetPostBySearchHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager)
			: base(repository, userManager, roleManager)
		{
			_minDate = DateTime.Parse("12/31/9999");
		}

		public async Task<IEnumerable<Post>> Handle(GetPostBySearchQuery request, CancellationToken cancellationToken)
		{
			DateTime fromDate;
			DateTime toDate;
			List<Post> posts = await _repository.All<Post>(
				p => p.IsCompleted == false && p.IsDeleted == false &&
				(!DateTime.TryParse(request.FromDate, out fromDate) || p.DateAndTime >= fromDate) &&
				(!DateTime.TryParse(request.ToDate, out toDate) || p.DateAndTime <= _minDate) &&
				p.FromDestinationCityId == request.FromDestinationCityId &&
				p.ToDestinationCityId == request.ToDestinationCityId &&
				(request.PriceMin == null || p.PricePerSeat >= request.PriceMin) &&
				(request.PriceMax == null || p.PricePerSeat <= request.PriceMax) &&
				(request.Baggage == null || p.Baggage == request.Baggage) &&
				(request.Pets == null || p.Pets == request.Pets))
				.ToListAsync();

			return await Task.FromResult(posts);
		}
	}
}
