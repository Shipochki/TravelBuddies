namespace TravelBuddies.Application.UserBlackList.Command.CreateUserBlackList
{
	using MediatR;
	using Microsoft.AspNetCore.Identity;
	using TravelBuddies.Application.Common.Interfaces.Repository;
	using TravelBuddies.Domain.Entities;

	public class CreateUserBlackListHandler : BaseHandler, IRequestHandler<CreateUserBlackListCommand, Task>
	{
		public CreateUserBlackListHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager)
			: base(repository, userManager, roleManager)
		{
		}

		public Task<Task> Handle(CreateUserBlackListCommand request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
