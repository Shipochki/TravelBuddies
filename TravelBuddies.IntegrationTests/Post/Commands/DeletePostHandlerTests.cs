namespace TravelBuddies.IntegrationTests.Post.Commands
{
    using Microsoft.AspNetCore.Identity;
    using TravelBuddies.Application.Common.Exceptions.NotFound;
    using TravelBuddies.Application.Post.Commands.DeletePost;
    using TravelBuddies.Domain.Common;
    using TravelBuddies.Domain.Entities;

    public class DeletePostHandlerTests : BaseHandlerTests
	{
		[Fact]
		public void DeletePost_WithNonExistingPost_ShouldThrowsException()
		{
			//Arrange
			var handler = new DeletePostHandler(_repostiory, _userManager, _roleManager);
			var command = new DeletePostCommand(1, "1");

			//Assert
			Assert.ThrowsAsync<PostNotFoundException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task DeletePost_WithNonExistingCreator_ShouldThrowsExcpetion()
		{
			//Arrange
			var handler = new DeletePostHandler(_repostiory, _userManager, _roleManager);

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

			await _dbContext.AddAsync(post);
			await _dbContext.SaveChangesAsync();

			var command = new DeletePostCommand(post.Id, "1");

			//Assert
			await Assert.ThrowsAsync<ApplicationUserNotFoundException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task DeletePost_WithValidCreator_ShouldSetIsDeletedToTrue()
		{
			//Arrange
			var handler = new DeletePostHandler(_repostiory, _userManager, _roleManager);

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

			await _dbContext.AddAsync(post);
			await _dbContext.SaveChangesAsync();

			var command = new DeletePostCommand(post.Id, user.Id);

			//Act
			await handler.Handle(command, default);

			//Assert
			Assert.True(post.IsDeleted);
		}
	}
}
