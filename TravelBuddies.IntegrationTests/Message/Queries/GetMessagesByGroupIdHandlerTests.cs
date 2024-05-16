namespace TravelBuddies.IntegrationTests.Message.Queries
{
    using TravelBuddies.Application.Common.Exceptions.NotFound;
    using TravelBuddies.Application.Message.Queries.GetMessagesByGroupId;
    using TravelBuddies.Domain.Entities;

    public class GetMessagesByGroupIdHandlerTests : BaseHandlerTests
	{
		[Fact]
		public void GetMessagesByGroupId_WithNonExistingUser_ShouldThrowsException()
		{
			//Arrange
			var handler = new GetMessagesByGroupIdHandler(_repostiory, _userManager, _roleManager);
			var query = new GetMessagesByGroupIdQuery(1, "1");

			//Assert
			Assert.ThrowsAsync<ApplicationUserNotFoundException>(async () =>
			{
				await handler.Handle(query, default);
			});
		}

		[Fact]
		public async Task GetMessagesByGroupId_WithNonExistingGroup_ShouldThrowsException()
		{
			//Arrange
			var handler = new GetMessagesByGroupIdHandler(_repostiory, _userManager, _roleManager);

			var user = new ApplicationUser() { UserName = "test", Email = "test" };

			await _dbContext.AddAsync(user);
			await _dbContext.SaveChangesAsync();

			var query = new GetMessagesByGroupIdQuery(1, user.Id);

			//Assert
			await Assert.ThrowsAsync<GroupNotFoundException>(async () =>
			{
				await handler.Handle(query, default);
			});
		}

		[Fact]
		public async Task GetMessagesByGroupId_WithNonExistingUserGroup_ShouldThrowsException()
		{
			//Arrange
			var handler = new GetMessagesByGroupIdHandler(_repostiory, _userManager, _roleManager);

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
				ToDestinationCity = city2
			};
			var group = new Group()
			{
				Creator = user,
				CreatorId = user.Id,
				Post = post,
				PostId = post.Id,
				CreatedOn = DateTime.UtcNow,
			};
			var message = new Message()
			{
				Group = group,
				Creator = user,
				CreatorId = user.Id,
				Text = "test",
			};
			var user2 = new ApplicationUser { UserName = "test", Email = "test" };

			await _dbContext.AddAsync(user2);
			await _dbContext.AddAsync(message);
			await _dbContext.SaveChangesAsync();

			var command = new GetMessagesByGroupIdQuery(group.Id, user2.Id);

			//Assert
			await Assert.ThrowsAsync<ApplicationUserNotInGroupException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task GetMessagesByGroupId_WithValidData_ShouldReturnMessages()
		{
			//Arrange
			var handler = new GetMessagesByGroupIdHandler(_repostiory, _userManager, _roleManager);

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
				ToDestinationCity = city2
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
				User = user,
				UserId = user.Id,
			};
			var message = new Message()
			{
				Group = group,
				Creator = user,
				CreatorId = user.Id,
				Text = "test",
			};
			
			await _dbContext.AddAsync(message);
			await _dbContext.AddAsync(userGroup);
			await _dbContext.SaveChangesAsync();

			var command = new GetMessagesByGroupIdQuery(group.Id, user.Id);

			//Act
			var result = await handler.Handle(command, default);
			
			//Assert
			Assert.Equal(message, result.First());
		}
	}
}
