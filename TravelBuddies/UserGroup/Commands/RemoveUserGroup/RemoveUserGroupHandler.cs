namespace TravelBuddies.Application.UserGroup.Commands.RemoveUserGroup
{
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using System.Threading;
    using TravelBuddies.Domain.Common;
    using TravelBuddies.Domain.Entities;
    using static TravelBuddies.Application.Common.Exceptions.Messages.ExceptionMessages;
    using TravelBuddies.Application.Common.Interfaces.Repository;
	using TravelBuddies.Application.Common.Exceptions.NotFound;
	using TravelBuddies.Application.Common.Exceptions.Forbidden;

	public class RemoveUserGroupHandler : BaseHandler, IRequestHandler<RemoveUserGroupCommand, Task>
	{
		public RemoveUserGroupHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager)
			: base(repository, userManager, roleManager)
		{
		}

		public async Task<Task> Handle(RemoveUserGroupCommand request, CancellationToken cancellationToken)
		{
			ApplicationUser? userForRemove = await _userManager.FindByIdAsync(request.UserIdForRemove);

			if (userForRemove == null)
			{
				throw new ApplicationUserNotFoundException(
					string.Format(ApplicationUserNotFoundMessage, request.UserIdForRemove));
			}

			ApplicationUser? owner = await _userManager.FindByIdAsync(request.OwnerId);

			if (owner == null)
			{
				throw new ApplicationUserNotFoundException(
					string.Format(ApplicationUserNotFoundMessage, request.OwnerId));
			}

			Group? group = await _repository.GetByIdAsync<Group>(request.GroupId);

			if (group == null)
			{
				throw new GroupNotFoundException(
					string.Format(GroupNotFoundMessage, request.GroupId));
			}

			if (group.CreatorId != request.OwnerId
				&& !await _userManager.IsInRoleAsync(owner, ApplicationRoles.Admin))
			{
				throw new ApplicationUserNotCreatorException(
					string.Format(ApplicationUserNotCreatorMessage, request.OwnerId));
			}

			UserGroup? userGroup = await _repository
				.FirstOrDefaultAsync<UserGroup>(u => u.GroupId == request.GroupId && u.UserId == request.UserIdForRemove);

			if (userGroup == null)
			{
				throw new ApplicationUserNotInGroupException(
					string.Format(ApplicationUserNotInGroupMessage, request.UserIdForRemove, request.GroupId));
			}

			_repository.Delete(userGroup);
			await _repository.SaveChangesAsync();
			return Task.CompletedTask;
		}
	}
}
