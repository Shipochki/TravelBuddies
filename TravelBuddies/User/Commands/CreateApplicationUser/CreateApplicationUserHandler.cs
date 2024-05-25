namespace TravelBuddies.Application.User.Commands.CreateApplicationUser
{
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using System.Threading;
    using TravelBuddies.Domain.Common;
    using TravelBuddies.Domain.Entities;
    using static TravelBuddies.Application.Common.Exceptions.Messages.ExceptionMessages;
    using TravelBuddies.Application.Common.Interfaces.AzureStorage;
    using TravelBuddies.Application.Common.Interfaces.Repository;
	using TravelBuddies.Application.Common.Exceptions.BadRequest;
	using TravelBuddies.Application.Common.Interfaces.MailSender;

	public class CreateApplicationUserHandler : BaseHandler, IRequestHandler<CreateApplicationUserCommand, Task>
	{
		//private readonly IBlobService _blobService;
		private readonly IMailSender _mailSender;

		public CreateApplicationUserHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager
			//, IBlobService blobService
			, IMailSender mailSender)
			: base(repository, userManager, roleManager)
		{
			//_blobService = blobService;
			_mailSender = mailSender;
		}

		public async Task<Task> Handle(CreateApplicationUserCommand request, CancellationToken cancellationToken)
		{
			string profilePictureLink = string.Empty;

			if(request.ProfilePicture != null)
			{
				//profilePictureLink = await _blobService.UploadImageAsync(request.ProfilePicture);
			}

			ApplicationUser applicationUser = new ApplicationUser()
			{
				UserName = request.Email,
				Email = request.Email,
				FirstName = request.FirstName,
				LastName = request.LastName,
				City = request.City,
				Country = request.Country,
				ProfilePictureLink = profilePictureLink,
				CreatedOn = DateTime.Now
			};

			IdentityResult result = await _userManager.CreateAsync(applicationUser, request.Password);

			if (!result.Succeeded)
			{
				throw new UnableToCreateApplicationUserException(UnableToCreateApplicationUserMessage);
			}

			await _userManager.AddToRoleAsync(applicationUser, ApplicationRoles.Client);

			string body = _mailSender.GenerateRegistrationEmailMessage($"{applicationUser.FirstName} {applicationUser.LastName}");
			_mailSender.SendMessage("Succesful register", body, applicationUser.Email);

			return Task.CompletedTask;
		}
	}
}
