namespace TravelBuddies.IntegrationTests.User.Commands
{
    using TravelBuddies.Application.Common.Exceptions.BadRequest;
    using TravelBuddies.Application.Common.Interfaces.AzureStorage;
	using TravelBuddies.Application.Common.Interfaces.MailSender;
	using TravelBuddies.Application.User.Commands.CreateApplicationUser;
	using TravelBuddies.IntegrationTests.Helpers;

    public class CreateApplicationUserHandlerTests : BaseHandlerTests
	{
		private readonly IBlobService _blobService;
		private readonly IMailSender _mailSender;

        public CreateApplicationUserHandlerTests()
        {
			_blobService = new BlobServiceDummy();
			_mailSender = new MailSenderDummy();
        }

        [Fact]
		public void CreateApplicationUser_WithNotSuccessfulCreation_ShouldThrowsException()
		{
			//Arrange
			var handler = new CreateApplicationUserHandler(_repostiory, _userManager, _roleManager, _mailSender);

			var command = new CreateApplicationUserCommand()
			{
				Email = "email",
				FirstName = "name",
				LastName = "name",
				Password = "",
			};

			//Assert
			Assert.ThrowsAsync<UnableToCreateApplicationUserException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}
	}
}
