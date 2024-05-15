namespace TravelBuddies.Application.User.Commands.DeleteApplicationUser
{
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using System.Threading;
    using TravelBuddies.Application.Common.Repository;
    using TravelBuddies.Application.Common.Exceptions;
    using TravelBuddies.Domain.Entities;
    using static TravelBuddies.Application.Common.Exceptions.Messages.ExceptionMessages;

    public class DeleteApplicationUserHandler : BaseHandler, IRequestHandler<DeleteApplicationUserCommand, Task>
	{
		public DeleteApplicationUserHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager)
			: base(repository, userManager, roleManager)
		{
		}

		public async Task<Task> Handle(DeleteApplicationUserCommand request, CancellationToken cancellationToken)
		{
			ApplicationUser? applicationUser = await _userManager.FindByIdAsync(request.Id);

			if (applicationUser == null)
			{
				throw new ApplicationUserNotFoundException(
					string.Format(ApplicationUserNotFoundMessage, request.Id));
			}

			applicationUser.IsDeleted = true;
			applicationUser.DeletedOn = DateTime.Now;
			applicationUser.FirstName = null;
			applicationUser.LastName = null;
			applicationUser.Email = null;
			applicationUser.PhoneNumber = null;
			applicationUser.UserName = null;
			applicationUser.ProfilePictureLink = null;
			applicationUser.DriverLicenseBackPictureLink = null;
			applicationUser.DriverLicenseFrontPictureLink = null;

			await _repository.SaveChangesAsync();

			return Task.CompletedTask;
		}
	}
}
