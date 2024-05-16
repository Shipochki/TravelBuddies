namespace TravelBuddies.Application.Group.Commands.DeleteGroup
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

			await _repository.SaveChangesAsync();

			return Task.CompletedTask;
		}
	}
}
