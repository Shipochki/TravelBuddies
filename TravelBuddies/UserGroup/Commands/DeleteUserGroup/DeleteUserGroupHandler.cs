namespace TravelBuddies.Application.UserGroup.Commands.DeleteUserGroup
{
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using System.Threading;
    using TravelBuddies.Application.Exceptions;
    using TravelBuddies.Application.Repository;
    using TravelBuddies.Domain.Entities;
    using static TravelBuddies.Application.Exceptions.Messages.ExceptionMessages;

    public class DeleteUserGroupHandler : BaseHandler, IRequestHandler<DeleteUserGroupCommand, Task>
	{
		public DeleteUserGroupHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager)
			: base(repository, userManager, roleManager)
		{
		}

		public async Task<Task> Handle(DeleteUserGroupCommand request, CancellationToken cancellationToken)
		{
			UserGroup? userGroup = await _repository
				.FirstOrDefaultAsync<UserGroup>(u => u.GroupId == request.GroupId && u.UserId == request.UserId);

			if(userGroup == null)
			{
				throw new ApplicationUserNotInGroupException(
					string.Format(ApplicationUserNotInGroupMessage, request.UserId, request.GroupId));
			}

			_repository.Delete(userGroup);
			await _repository.SaveChangesAsync();
			return Task.CompletedTask;
		}
	}
}
