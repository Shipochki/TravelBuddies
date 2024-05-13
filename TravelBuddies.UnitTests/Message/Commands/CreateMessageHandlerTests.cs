namespace TravelBuddies.UnitTests.Message.Commands
{
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Message.Commands.CreateMessage;
	using TravelBuddies.Domain.Entities;

	public class CreateMessageHandlerTests : BaseHandlerTests
	{
		[Fact]
		public void CreateMessage_WithNonExistingCreator_ShouldThrowsException()
		{
			//Arrange
			var handler = new CreateMessageHandler(_repostiory, _userManager, _roleManager);
			var command = new CreateMessageCommand()
			{
				CreatorId = "1",
				Text = "Test"
			};

			//Assert
			Assert.ThrowsAsync<ApplicationUserNotFoundException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task CreateMessage_WithNonExistingGroup_ShouldThrowsException()
		{
			//Arrange
			var handler = new CreateMessageHandler(_repostiory, _userManager, _roleManager);

			var user = new ApplicationUser() { UserName = "Test", Email = "Test" };

			await _dbContext.AddAsync(user);
			await _dbContext.SaveChangesAsync();

			var command = new CreateMessageCommand()
			{
				CreatorId = user.Id,
				Text = "Test",
				GroupId = 1,
			};

			//Assert
			await Assert.ThrowsAsync<GroupNotFoundException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact] 
		public async Task CreateMessage_WithUserNotInGroup_ShouldThrowsException()
		{
			//Arrange
			var handler = new CreateMessageHandler(_repostiory, _userManager, _roleManager);

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

			await _dbContext.AddAsync(group);
			await _dbContext.SaveChangesAsync();

			var command = new CreateMessageCommand()
			{
				CreatorId = user.Id,
				GroupId = group.Id,
				Text = "test",
			};

			//Assert
			await Assert.ThrowsAsync<ApplicationUserNotInGroupException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task CreateMessage_WithCorrectData_ShouldCreateMessage()
		{
			//Arrange
			var handler = new CreateMessageHandler(_repostiory, _userManager, _roleManager);

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

			await _dbContext.AddAsync(userGroup);
			await _dbContext.SaveChangesAsync();

			var command = new CreateMessageCommand()
			{
				CreatorId = user.Id,
				GroupId = group.Id,
				Text = "test",
			};

			//Act
			var result = await handler.Handle(command, default);

			//Assert
			Assert.NotNull(result);
			Assert.Equal(user, result.Creator);
			Assert.Equal(group, result.Group);
		}
	}
}
