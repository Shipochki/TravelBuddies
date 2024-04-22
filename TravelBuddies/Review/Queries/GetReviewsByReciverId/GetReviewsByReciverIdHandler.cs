﻿namespace TravelBuddies.Application.Review.Queries.GetReviewsByReciverId
{
	using MediatR;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.EntityFrameworkCore;
	using System.Threading;
	using System.Threading.Tasks;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;

	public class GetReviewsByReciverIdHandler : BaseHandler, IRequestHandler<GetReviewsByReciverIdQuery, List<Review>>
	{
		public GetReviewsByReciverIdHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager)
			: base(repository, userManager, roleManager)
		{
		}

		public async Task<List<Review>> Handle(GetReviewsByReciverIdQuery request, CancellationToken cancellationToken)
		{
			List<Review> reviews = await _repository
				.All<Review>(r => r.IsDeleted == false && r.ReciverId == request.Id)
				.ToListAsync();

			return await Task.FromResult(reviews);
		}
	}
}