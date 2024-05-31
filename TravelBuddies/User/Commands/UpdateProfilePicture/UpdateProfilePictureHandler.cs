namespace TravelBuddies.Application.User.Commands.UpdateProfilePicture
{
	using MediatR;
	using Microsoft.AspNetCore.Identity;
	using TravelBuddies.Application.Common.Exceptions.NotFound;
	using TravelBuddies.Application.Common.Interfaces.AzureStorage;
	using TravelBuddies.Application.Common.Interfaces.Repository;
	using TravelBuddies.Domain.Entities;
	using static TravelBuddies.Application.Common.Exceptions.Messages.ExceptionMessages;

	public class UpdateProfilePictureHandler : BaseHandler, IRequestHandler<UpdateProfilePictureCommand, Task>
	{
		private readonly IBlobService _blobService;

		public UpdateProfilePictureHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager
			, IBlobService blobService)
			: base(repository, userManager, roleManager)
		{
			_blobService = blobService;
		}

		public async Task<Task> Handle(UpdateProfilePictureCommand request, CancellationToken cancellationToken)
		{
			ApplicationUser? user = await _userManager.FindByIdAsync(request.UserId);

			if (user == null)
			{
				throw new ApplicationUserNotFoundException(
					string.Format(ApplicationUserNotFoundMessage, request.UserId));
			}

			user.ProfilePictureLink = await _blobService.UploadImageAsync(request.ProfilePicture);

			await _repository.SaveChangesAsync();

			return Task.CompletedTask;
		}
	}
}
