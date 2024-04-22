namespace TravelBuddies.Application.User.Commands.CreateApplicationUser
{
	using MediatR;
	using Microsoft.AspNetCore.Identity;
	using System.Threading;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;

	public class CreateApplicationUserHandler : BaseHandler, IRequestHandler<CreateApplicationUserCommand, Task>
	{
		public CreateApplicationUserHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager)
			: base(repository, userManager, roleManager)
		{
		}

		public Task<Task> Handle(CreateApplicationUserCommand request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
