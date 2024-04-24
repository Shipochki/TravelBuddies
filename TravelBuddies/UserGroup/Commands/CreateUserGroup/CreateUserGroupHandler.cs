namespace TravelBuddies.Application.UserGroup.Commands.CreateUserGroup
{
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System.Threading;
	using TravelBuddies.Application.Constants;
	using TravelBuddies.Application.Exceptions;
    using TravelBuddies.Application.Repository;
    using TravelBuddies.Domain.Entities;
    using static TravelBuddies.Application.Exceptions.Messages.ExceptionMessages;

    public class CreateUserGroupHandler : BaseHandler, IRequestHandler<CreateUserGroupCommand, Task>
	{
		public CreateUserGroupHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager)
			: base(repository, userManager, roleManager)
		{
		}

		public async Task<Task> Handle(CreateUserGroupCommand request, CancellationToken cancellationToken)
		{
			ApplicationUser? user = await _userManager.FindByIdAsync(request.UserId);

			if (user == null)
			{
				throw new ApplicationUserNotFoundException(
					string.Format(ApplicationUserNotFoundMessage, request.UserId));
			}

			Group? group = await _repository.GetByIdAsync<Group>(request.GroupId);

			if (group == null)
			{
				throw new GroupNotFoundException(
					string.Format(GroupNotFoundMessage, request.GroupId));
			}

			UserGroup? userGroup = await _repository
				.FirstOrDefaultAsync<UserGroup>(u => u.GroupId == request.GroupId && u.UserId == request.UserId);

			if (userGroup != null)
			{
				throw new ApplicationUserAllreadyInGroupException(
					string.Format(ApplicationUserAllreadyInGroupMessage, request.UserId, request.GroupId));
			}

			Post? post = await _repository.GetByIdAsync<Post>(group.PostId);

			if(post == null)
			{
				throw new PostNotFoundException(
					string.Format(PostNotFoundMessage, group.PostId));
			}

			List<UserGroup> userGroups = await _repository
				.AllReadonly<UserGroup>(u => u.GroupId == request.GroupId)
				.ToListAsync();

			if(post.FreeSeats <= userGroups.Count 
				&& !await _userManager.IsInRoleAsync(user, ApplicationRoles.Admin))
			{
				throw new NotAvailableSeatsInPostException(
					string.Format(NotAvailableSeatsInPostMessage, post.Id));
			}

			userGroup = new UserGroup()
			{
				User = user,
				UserId = user.Id,
				Group = group,
				GroupId = group.Id
			};

			await _repository.AddAsync(userGroup);
			await _repository.SaveChangesAsync();

			return Task.CompletedTask;
		}
	}
}
