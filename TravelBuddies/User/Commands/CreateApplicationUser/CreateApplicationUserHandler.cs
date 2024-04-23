namespace TravelBuddies.Application.User.Commands.CreateApplicationUser
{
	using MediatR;
	using Microsoft.AspNetCore.Identity;
	using System;
	using System.Threading;
	using TravelBuddies.Application.Constants;
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;
	using static TravelBuddies.Application.Exceptions.Messages.ExceptionMessages;

	public class CreateApplicationUserHandler : BaseHandler, IRequestHandler<CreateApplicationUserCommand, Task>
	{
		public CreateApplicationUserHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager)
			: base(repository, userManager, roleManager)
		{
		}

		public async Task<Task> Handle(CreateApplicationUserCommand request, CancellationToken cancellationToken)
		{
			ApplicationUser applicationUser = new ApplicationUser()
			{
				UserName = request.Email,
				Email = request.Email,
				FirstName = request.FirstName,
				LastName = request.LastName,
				City = request.City,
				Country = request.Country,
			};

			IdentityResult result = await _userManager.CreateAsync(applicationUser, request.Password);

			if (!result.Succeeded)
			{
				throw new UnableToCreateApplicationUserException(UnableToCreateApplicationUserMessage);
			}

			await _userManager.AddToRoleAsync(applicationUser, ApplicationRoles.Client);

			return Task.CompletedTask;
		}
	}
}
