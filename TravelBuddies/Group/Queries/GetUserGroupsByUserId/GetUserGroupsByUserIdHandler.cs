namespace TravelBuddies.Application.Group.Queries.GetUserGroupsByUserId
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

	public class GetUserGroupsByUserIdHandler : BaseHandler, IRequestHandler<GetUserGroupsByUserIdQuery, List<Group>>
	{
		public GetUserGroupsByUserIdHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager)
			: base(repository, userManager, roleManager)
		{
		}

		public async Task<List<Group>> Handle(GetUserGroupsByUserIdQuery request, CancellationToken cancellationToken)
		{
			ApplicationUser? user = await _userManager.FindByIdAsync(request.UserId);

			if (user == null)
			{
				throw new ApplicationUserNotFoundException(
					string.Format(ApplicationUserNotFoundMessage, request.UserId));
			}

			List<Group> groups = await _repository
				.AllReadonly<Group>(g => g.UsersGroups.Any(u => u.UserId == request.UserId) && g.IsDeleted == false)
				.Include(g => g.Creator)
				.Include(g => g.UsersGroups)
				.Include(g => g.Post)
				.ThenInclude(g => g.FromDestinationCity)
				.Include(g => g.Post)
				.ThenInclude(g => g.ToDestinationCity)
				.OrderByDescending(g => g.Post.DateAndTime)
				.ToListAsync();

			return await Task.FromResult(groups);
		}
	}
}
