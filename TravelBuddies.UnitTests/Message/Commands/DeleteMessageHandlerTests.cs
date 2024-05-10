namespace TravelBuddies.UnitTests.Message.Commands
{
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Message.Commands.DeleteMessage;
	using TravelBuddies.Domain.Entities;

	public class DeleteMessageHandlerTests : BaseHandlerTests
	{
		[Fact]
		public void DeleteMessage_WithNonExisitingMessage_ShouldThrowsException()
		{
			//Arrange
			var handler = new DeleteMessageHandler(_repostiory, _userManager, _roleManager);
			var command = new DeleteMessageCommand(1, "1");

			//Assert
			Assert.ThrowsAsync<MessageNotFoundException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task DeleteMessage_WithNonExistingCreator_ShouldThrowsExcpetion()
		{
			//Arrange
			var handler = new DeleteMessageHandler(_repostiory, _userManager, _roleManager);

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

			await _dbContext.AddAsync(message);
			await _dbContext.SaveChangesAsync();

			var command = new DeleteMessageCommand(message.Id, "1");

			//Assert
			await Assert.ThrowsAsync<ApplicationUserNotFoundException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task DeleteMessage_WithNonMatchingCreator_ShouldThrowsExcpetion()
		{
			//Arrange
			var handler = new DeleteMessageHandler(_repostiory, _userManager, _roleManager);

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

			var command = new DeleteMessageCommand(message.Id, user2.Id);

			//Assert
			await Assert.ThrowsAsync<ApplicationUserNotCreatorException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task DeleteMessage_WithValidData_ShouldSetIsDeletedToTrue()
		{
			//Arrange
			var handler = new DeleteMessageHandler(_repostiory, _userManager, _roleManager);

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

			await _dbContext.AddAsync(message);
			await _dbContext.SaveChangesAsync();

			var command = new DeleteMessageCommand(message.Id, user.Id);

			//Act
			await handler.Handle(command, default);

			//Assert
			Assert.True(message.IsDeleted);
		}
	}
}
