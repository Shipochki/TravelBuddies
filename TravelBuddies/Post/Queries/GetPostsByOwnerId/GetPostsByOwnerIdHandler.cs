namespace TravelBuddies.Application.Post.Queries.GetPostsByOwnerId
{
	using MediatR;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.EntityFrameworkCore;
	using System.Threading;
	using System.Threading.Tasks;
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;
	using static TravelBuddies.Application.Exceptions.Messages.ExceptionMessages;

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
				.All<Post>(p => p.IsDeleted == false && p.CreatorId == request.OwnerId)
				.Include(p => p.FromDestinationCity)
				.Include(p => p.ToDestinationCity)
				.ToListAsync();

			return await Task.FromResult(posts);
		}
	}
}
