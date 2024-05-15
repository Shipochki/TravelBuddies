namespace TravelBuddies.Application.Group.Queries.GetGroupById
{
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System.Threading;
    using System.Threading.Tasks;
    using TravelBuddies.Application.Common.Exceptions;
    using TravelBuddies.Domain.Entities;
    using static TravelBuddies.Application.Common.Exceptions.Messages.ExceptionMessages;
    using TravelBuddies.Application.Common.Interfaces.Repository;

    public class GetGroupByIdHandler : BaseHandler, IRequestHandler<GetGroupByIdQuery, Group>
	{
		public GetGroupByIdHandler(IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager)
			: base(repository, userManager, roleManager)
		{
		}

		public async Task<Group> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
		{
			ApplicationUser? user = await _userManager.FindByIdAsync(request.UserId);

			if (user == null)
			{
				throw new ApplicationUserNotFoundException(
					string.Format(ApplicationUserNotFoundMessage, request.UserId));
			}

			UserGroup? userGroup = await _repository
				.FirstOrDefaultAsync<UserGroup>(u => u.UserId == request.UserId && u.GroupId == request.GroupId);

			if (userGroup == null)
			{
				throw new ApplicationUserNotInGroupException(
					string.Format(ApplicationUserNotInGroupMessage, request.UserId, request.GroupId));
			}

			Group? group = await _repository
				.All<Group>()
				.Include(g => g.Creator)
				.Include(g => g.UsersGroups)
				.ThenInclude(u => u.User)
				.Include(g => g.Messages)
				.ThenInclude(m => m.Creator)
				.FirstOrDefaultAsync(g => g.Id == request.GroupId);

			if (group == null)
			{
				throw new GroupNotFoundException(
					string.Format(GroupNotFoundMessage, request.GroupId));
			}

			return await Task.FromResult(group);
		}
	}
}
