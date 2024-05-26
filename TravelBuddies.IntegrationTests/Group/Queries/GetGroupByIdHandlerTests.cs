namespace TravelBuddies.IntegrationTests.Group.Queries
{
    using TravelBuddies.Application.Common.Exceptions.Forbidden;
    using TravelBuddies.Application.Common.Exceptions.NotFound;
    using TravelBuddies.Application.Group.Queries.GetGroupById;
    using TravelBuddies.Domain.Entities;

    public class GetGroupByIdHandlerTests : BaseHandlerTests
	{
		[Fact]
		public void GetGroupById_WithNonExistingUser_ShouldThrowsException()
		{
			//Arrange
			var handler = new GetGroupByIdHandler(_repostiory, _userManager, _roleManager);
			var qeury = new GetGroupByIdQuery(1, "1");

			//Assert
			Assert.ThrowsAsync<ApplicationUserNotFoundException>(async () =>
			{
				await handler.Handle(qeury, default);
			});
		}

		[Fact]
		public async Task GetGroupById_WithUserNotInGroup_ShouldThrowsException()
		{
			//Arrange
			var handler = new GetGroupByIdHandler(_repostiory, _userManager, _roleManager);

			var user = new ApplicationUser { UserName = "testuser", Email = "test2@example.com" };
			await _repostiory.AddAsync(user);
			await _repostiory.SaveChangesAsync();

			var qeury = new GetGroupByIdQuery(1, user.Id);

			//Assert
			await Assert.ThrowsAsync<ApplicationUserNotInGroupException>(async () =>
			{
				await handler.Handle(qeury, default);
			});
		}

		[Fact]
		public async Task GetGroupById_WithNonExistingGroup_ShouldThrowsException()
		{
			var handler = new GetGroupByIdHandler(_repostiory, _userManager, _roleManager);

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
				Currency = "Eur"
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

			await _repostiory.AddAsync(group);
			await _repostiory.SaveChangesAsync();

			var query = new GetGroupByIdQuery(group.Id, user.Id);

			//Assert
			await Assert.ThrowsAsync<ApplicationUserNotInGroupException>(async () =>
			{
				await handler.Handle(query, default);
			});
		}
	}
}
