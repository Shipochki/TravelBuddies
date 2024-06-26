﻿namespace TravelBuddies.Application.User.Commands.BecomeDriver
{
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using System.Threading;
    using TravelBuddies.Domain.Common;
    using TravelBuddies.Domain.Entities;
    using static TravelBuddies.Application.Common.Exceptions.Messages.ExceptionMessages;
    using Microsoft.IdentityModel.Tokens;
    using TravelBuddies.Application.Common.Interfaces.Repository;
	using TravelBuddies.Application.Common.Exceptions.NotFound;
	using TravelBuddies.Application.Common.Exceptions.BadRequest;

	public class BecomeDriverHandler : BaseHandler, IRequestHandler<BecomeDriverCommand, Task>
	{
		public BecomeDriverHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager)
			: base(repository, userManager, roleManager)
		{
		}

		public async Task<Task> Handle(BecomeDriverCommand request, CancellationToken cancellationToken)
		{
			ApplicationUser? applicationUser = await _userManager
				.FindByIdAsync(request.UserId);

			if (applicationUser == null)
			{
				throw new ApplicationUserNotFoundException(
					string.Format(ApplicationUserNotFoundMessage, request.UserId));
			}

			IdentityRole? identityRole = await _roleManager
				.FindByNameAsync(ApplicationRoles.Driver);

			if (identityRole == null
				|| identityRole.Name.IsNullOrEmpty())
			{
				throw new IdentityRoleNotFoundException(
					string.Format(IdentityRoleNotFoundMessage, ApplicationRoles.Driver));
			}

			await _userManager
				.RemoveFromRoleAsync(applicationUser, ApplicationRoles.Client);

			IdentityResult result = await _userManager
				.AddToRoleAsync(applicationUser, ApplicationRoles.Driver);

			if (!result.Succeeded)
			{
				throw new UnableToAddRoleToUserException(
					string.Format(UnableToAddRoleToUserMessage, request.UserId));
			}

			return Task.CompletedTask;
		}
	}
}
