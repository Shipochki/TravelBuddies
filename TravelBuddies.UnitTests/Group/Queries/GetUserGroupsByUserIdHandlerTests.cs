namespace TravelBuddies.UnitTests.Group.Queries
{
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Group.Queries.GetUserGroupsByUserId;
	using TravelBuddies.Domain.Entities;

	public class GetUserGroupsByUserIdHandlerTests : BaseHandlerTests
	{
		[Fact]
		public async Task GetUserGroupsByUserId_WithNonExistingUser_ShouldThrowsException()
		{
			//Arrange
			var handler = new GetUserGroupsByUserIdHandler(_repostiory, _userManager, _roleManager);
			var query = new GetUserGroupsByUserIdQuery("1");

			await Assert.ThrowsAsync<ApplicationUserNotFoundException>(async () =>
			{
				await handler.Handle(query, default);
			});
		}
	}
}
