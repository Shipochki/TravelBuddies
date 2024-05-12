namespace TravelBuddies.UnitTests.UserGroup.Commands
{
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.UserGroup.Commands.RemoveUserGroup;

	public class RemoveUserGroupHandlerTests : BaseHandlerTests
	{
		[Fact]
		public void RemoveUserGroup_WithNonExistingUserForRemove_ShouldThrowsException()
		{
			//Arrange
			var handler = new RemoveUserGroupHandler(_repostiory, _userManager, _roleManager);
			var command = new RemoveUserGroupCommand()
			{
				OwnerId = "1",
				UserIdForRemove = "1",
			};

			//Assert
			Assert.ThrowsAsync<ApplicationUserNotFoundException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}
	}
}
