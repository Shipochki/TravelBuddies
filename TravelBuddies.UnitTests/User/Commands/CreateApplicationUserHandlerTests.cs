namespace TravelBuddies.UnitTests.User.Commands
{
	using Microsoft.AspNetCore.Identity;
	using NSubstitute;
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Interfaces.AzureStorage;
	using TravelBuddies.Application.User.Commands.CreateApplicationUser;
	using TravelBuddies.Domain.Entities;
	using TravelBuddies.UnitTests.Helpers;

	public class CreateApplicationUserHandlerTests : BaseHandlerTests
	{
		private readonly IBlobService _blobService;

        public CreateApplicationUserHandlerTests()
        {
			_blobService = new BlobServiceFake();
        }

        [Fact]
		public void CreateApplicationUser_WithNotSuccessfulCreation_ShouldThrowsException()
		{
			//Arrange
			var handler = new CreateApplicationUserHandler(_repostiory, _userManager, _roleManager, _blobService);

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
