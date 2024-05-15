namespace TravelBuddies.Application.User.Commands.CreateApplicationUser
{
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using System.Threading;
    using TravelBuddies.Domain.Common;
    using TravelBuddies.Application.Common.Exceptions;
    using TravelBuddies.Domain.Entities;
    using static TravelBuddies.Application.Common.Exceptions.Messages.ExceptionMessages;
    using TravelBuddies.Application.Common.Repository;
    using TravelBuddies.Application.Common.Interfaces.AzureStorage;

    public class CreateApplicationUserHandler : BaseHandler, IRequestHandler<CreateApplicationUserCommand, Task>
	{
		private readonly IBlobService _blobService;

		public CreateApplicationUserHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager
			, IBlobService blobService)
			: base(repository, userManager, roleManager)
		{
			_blobService = blobService;
		}

		public async Task<Task> Handle(CreateApplicationUserCommand request, CancellationToken cancellationToken)
		{
			string profilePictureLink = string.Empty;

			if(request.ProfilePicture != null)
			{
				profilePictureLink = await _blobService.UploadImageAsync(request.ProfilePicture);
			}

			ApplicationUser applicationUser = new ApplicationUser()
			{
				UserName = request.Email,
				Email = request.Email,
				FirstName = request.FirstName,
				LastName = request.LastName,
				City = request.City,
				Country = request.Country,
				ProfilePictureLink = profilePictureLink
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
