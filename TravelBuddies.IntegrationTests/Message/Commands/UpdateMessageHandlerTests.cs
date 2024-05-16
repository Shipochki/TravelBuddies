namespace TravelBuddies.IntegrationTests.Message.Commands
{
    using TravelBuddies.Application.Common.Exceptions.BadRequest;
    using TravelBuddies.Application.Common.Exceptions.Forbidden;
    using TravelBuddies.Application.Common.Exceptions.NotFound;
    using TravelBuddies.Application.Message.Commands.UpdateMessage;
    using TravelBuddies.Domain.Entities;

    public class UpdateMessageHandlerTests : BaseHandlerTests
	{
		[Fact]
		public void UpdateMessage_WithNonExisitingMessage_ShouldThrowsException()
		{
			//Arrange
			var handler = new UpdateMessageHandler(_repostiory, _userManager, _roleManager);
			var command = new UpdateMessageCommand()
			{
				CreatorId = "1",
				Text = "1",
				Id = 1
			};

			//Assert
			Assert.ThrowsAsync<MessageNotFoundException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task UpdateMessage_WithNonMatchingCreator_ShouldThrowsException()
		{
			//Arrange
			var handler = new UpdateMessageHandler(_repostiory, _userManager, _roleManager);

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

			var command = new UpdateMessageCommand()
			{
				Id = message.Id,
				CreatorId = "1",
				Text = "Test",
			};

			//Assert
			await Assert.ThrowsAsync<ApplicationUserNotCreatorException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task UpdateMessage_WithNonMatchingGroup_ShouldThrowsException()
		{
			//Arrange
			var handler = new UpdateMessageHandler(_repostiory, _userManager, _roleManager);

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

			var command = new UpdateMessageCommand()
			{
				Id = message.Id,
				CreatorId = user.Id,
				Text = "Test",
				GroupId = 20
			};

			//Assert
			await Assert.ThrowsAsync<GroupNotMatchException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task UpdateMessage_WithValidData_ShouldUpdateText()
		{
			//Arrange
			var handler = new UpdateMessageHandler(_repostiory, _userManager, _roleManager);

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

			var command = new UpdateMessageCommand()
			{
				Id = message.Id,
				CreatorId = user.Id,
				Text = "TestUpdate",
				GroupId = group.Id,
			};

			//Act
			await handler.Handle(command, default);
			
			//Assert
			Assert.Equal(command.Text, message.Text);
		}
	}
}
