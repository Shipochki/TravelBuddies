namespace TravelBuddies.Application.Group.Commands.DeleteGroup
{
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using System.Threading;
    using TravelBuddies.Domain.Common;
    using TravelBuddies.Application.Exceptions;
    using TravelBuddies.Application.Repository;
    using TravelBuddies.Domain.Entities;
    using static TravelBuddies.Application.Exceptions.Messages.ExceptionMessages;

    public class DeleteGroupHandler : BaseHandler, IRequestHandler<DeleteGroupCommand, Task>
	{
		public DeleteGroupHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager)
			: base(repository, userManager, roleManager)
		{
		}

		public async Task<Task> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
		{
			Group? group = await _repository.GetByIdAsync<Group>(request.Id);

			if (group == null)
			{
				throw new GroupNotFoundException(
					string.Format(GroupNotFoundMessage, request.Id));
			}

			ApplicationUser? user = await _userManager.FindByIdAsync(request.CreatorId);

			if(user == null)
			{
				throw new ApplicationUserNotFoundException(
					string.Format(ApplicationUserNotFoundMessage, request.CreatorId));
			}

			if (group.CreatorId != request.CreatorId 
				&& !await _userManager.IsInRoleAsync(user, ApplicationRoles.Admin))
			{
				throw new ApplicationUserNotCreatorException(
					string.Format(ApplicationUserNotCreatorMessage, request.CreatorId));
			}

			group.IsDeleted = true;
			group.DeletedOn = DateTime.Now;

			_repository.Update(group);
			await _repository.SaveChangesAsync();

			return Task.CompletedTask;
		}
	}
}
