namespace TravelBuddies.IntegrationTests.Group.Queries
{
    using TravelBuddies.Application.Common.Exceptions.NotFound;
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

			//Assert
			await Assert.ThrowsAsync<ApplicationUserNotFoundException>(async () =>
			{
				await handler.Handle(query, default);
			});
		}

		[Fact]
		public async Task GetUserGroupsByUserId_WithExistingUser_ShouldReturnList()
		{
			//Arrange
			var handler = new GetUserGroupsByUserIdHandler(_repostiory, _userManager, _roleManager);

			var user = new ApplicationUser { UserName = "testuser", Email = "test@example.com" };
			var country = new Country() { Name = "country" };
			var city1 = new City() { Country = country, Name = "Sofia" };
			var city2 = new City() { Country = country, Name = "Targovishte" };
			var post = new Post
			{
				Creator = user,
				CreatorId = user.Id,
				Description = "test",
				FromDestinationCity = city1,
				ToDestinationCity = city2,
				Currency = "eur"
			};
			var group = new Group()
			{
				Creator = user,
				CreatorId = user.Id,
				Post = post,
				PostId = post.Id,
				CreatedOn = DateTime.UtcNow,
			};
			var userGroup = new UserGroup()
			{
				Group = group,
				GroupId = group.Id,
				User = user,
				UserId = user.Id,
			};

			await _dbContext.AddAsync(userGroup);
			await _dbContext.SaveChangesAsync();

			var command = new GetUserGroupsByUserIdQuery(user.Id);

			//Act
			var reuslt = await handler.Handle(command, default);

			//Assert
			Assert.Equal(group.Id, reuslt.First().Id);
			Assert.Single(reuslt);
		}
	}
}
