namespace TravelBuddies.Application.User.Commands.UpdateApplicationUser
{
	using MediatR;
	using Microsoft.AspNetCore.Identity;
	using TravelBuddies.Application.Common.Exceptions.NotFound;
	using TravelBuddies.Application.Common.Interfaces.Repository;
	using TravelBuddies.Domain.Entities;
	using static TravelBuddies.Application.Common.Exceptions.Messages.ExceptionMessages;

	public class UpdateApplicationUserHandler : BaseHandler, IRequestHandler<UpdateApplicationUserCommand, Task>
	{
		public UpdateApplicationUserHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager)
			: base(repository, userManager, roleManager)
		{
		}

		public async Task<Task> Handle(UpdateApplicationUserCommand request, CancellationToken cancellationToken)
		{
			ApplicationUser? user = await _userManager
				.FindByIdAsync(request.Id);

			if (user == null)
			{
				throw new ApplicationUserNotFoundException(
					string.Format(ApplicationUserNotFoundMessage, request.Id));
			}

			user.FirstName = request.FirstName;
			user.LastName = request.LastName;
			user.City = request.City;
			user.Country = request.Country;

			await _repository.SaveChangesAsync();

			return Task.CompletedTask;
		}
	}
}
