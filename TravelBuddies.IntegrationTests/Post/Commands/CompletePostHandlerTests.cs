namespace TravelBuddies.IntegrationTests.Post.Commands
{
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Post.Commands.CompletePost;
	using TravelBuddies.Domain.Entities;

	public class CompletePostHandlerTests : BaseHandlerTests
	{
		[Fact]
		public void CompletePost_WithNonExistingPost_ShouldThrowsException()
		{
			//Arrange
			var handler = new CompletePostHandler(_repostiory, _userManager, _roleManager);
			var command = new CompletePostCommand(1, "1");

			//Assert
			Assert.ThrowsAsync<PostNotFoundException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task CompletePost_WithNonMatchingCreator_ShouldThrowsException()
		{
			//Arrange
			var handler = new CompletePostHandler(_repostiory, _userManager, _roleManager);

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

			await _dbContext.AddAsync(post);
			await _dbContext.SaveChangesAsync();

			var command = new CompletePostCommand(post.Id, "1");

			//Assert
			await Assert.ThrowsAsync<ApplicationUserNotCreatorException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task CompletePost_WithValidData_ShouldSetIsCompletedToTrue()
		{
			//Arrange
			var handler = new CompletePostHandler(_repostiory, _userManager, _roleManager);

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

			await _dbContext.AddAsync(post);
			await _dbContext.SaveChangesAsync();

			var command = new CompletePostCommand(post.Id, user.Id);

			//Act
			await handler.Handle(command, default);

			//Assert
			Assert.True(post.IsCompleted);
		}
	}
}
