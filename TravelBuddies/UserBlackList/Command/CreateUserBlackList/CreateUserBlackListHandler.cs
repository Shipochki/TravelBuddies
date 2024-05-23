namespace TravelBuddies.Application.UserBlackList.Command.CreateUserBlackList
{
	using MediatR;
	using Microsoft.AspNetCore.Identity;
	using TravelBuddies.Application.Common.Exceptions.NotFound;
	using TravelBuddies.Application.Common.Interfaces.Repository;
	using static TravelBuddies.Application.Common.Exceptions.Messages.ExceptionMessages;
	using TravelBuddies.Domain.Entities;
	using TravelBuddies.Application.Common.Exceptions.BadRequest;
	using TravelBuddies.Application.Common.Exceptions.Forbidden;
	using TravelBuddies.Domain.Common;

	public class CreateUserBlackListHandler : BaseHandler, IRequestHandler<CreateUserBlackListCommand, Task>
	{
		public CreateUserBlackListHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager)
			: base(repository, userManager, roleManager)
		{
		}

		public async Task<Task> Handle(CreateUserBlackListCommand request, CancellationToken cancellationToken)
		{
			ApplicationUser? user = await _userManager.FindByIdAsync(request.UserId);

			if (user == null)
			{
				throw new ApplicationUserNotFoundException(
					string.Format(ApplicationUserNotFoundMessage, request.UserId));
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

			UserBlackList? blackList = await _repository
				.FirstOrDefaultAsync<UserBlackList>(b => b.UserId == request.UserId && b.GroupId == request.GroupId);

			if(blackList == null)
			{
				throw new ApplicationUserAllreadyIsBannedFromGroupException(
					string.Format(ApplicationUserAllreadyIsBannedFromGroup, request.UserId, request.GroupId);
			}

			UserBlackList userBlackList = new UserBlackList()
			{
				Group = group,
				GroupId = request.GroupId,
				User = user,
				UserId = request.UserId,
			};

			await _repository.AddAsync(userBlackList);
			await _repository.SaveChangesAsync();

			return Task.CompletedTask;
		}
	}
}
