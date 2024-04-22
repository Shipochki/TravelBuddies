﻿namespace TravelBuddies.Application.Group.Commands.DeleteGroup
{
	using MediatR;
	using Microsoft.AspNetCore.Identity;
	using System.Threading;
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;
	using static TravelBuddies.Application.Exceptions.ExceptionMessages;

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
				throw new GroupNotFoundException(string.Format(GroupNotFoundMessage, request.Id));
			}

			if (group.CreatorId != request.CreatorId)
			{
				throw new ApplicationUserNotCreatorException(string.Format(ApplicationUserNotCreatorMessage, request.CreatorId));
			}

			group.IsDeleted = true;
			group.DeletedOn = DateTime.Now;

			_repository.Update(group);
			await _repository.SaveChangesAsync();

			return Task.CompletedTask;
		}
	}
}
