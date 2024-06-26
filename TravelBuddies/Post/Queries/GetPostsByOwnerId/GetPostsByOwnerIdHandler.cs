﻿namespace TravelBuddies.Application.Post.Queries.GetPostsByOwnerId
{
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System.Threading;
    using System.Threading.Tasks;
    using TravelBuddies.Domain.Entities;
    using static TravelBuddies.Application.Common.Exceptions.Messages.ExceptionMessages;
    using TravelBuddies.Application.Common.Interfaces.Repository;
	using TravelBuddies.Application.Common.Exceptions.NotFound;

	public class GetPostsByOwnerIdHandler : BaseHandler, IRequestHandler<GetPostsByOwnerIdQuery, List<Post>>
	{
		public GetPostsByOwnerIdHandler(IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager)
			: base(repository, userManager, roleManager)
		{
		}

		public async Task<List<Post>> Handle(GetPostsByOwnerIdQuery request, CancellationToken cancellationToken)
		{
			ApplicationUser? user = await _userManager.FindByIdAsync(request.OwnerId);

			if (user == null)
			{
				throw new ApplicationUserNotFoundException(
					string.Format(ApplicationUserNotFoundMessage, request.OwnerId));
			}

			List<Post> posts = await _repository
				.AllReadonly<Post>(
					p => p.IsDeleted == false
					&& p.CreatorId == request.OwnerId
					&& p.IsCompleted == false)
				.Include(p => p.FromDestinationCity)
				.Include(p => p.ToDestinationCity)
				.OrderByDescending(p => p.DateAndTime)
				.ToListAsync();

			return await Task.FromResult(posts);
		}
	}
}
