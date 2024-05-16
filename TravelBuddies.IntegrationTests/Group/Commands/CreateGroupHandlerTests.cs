namespace TravelBuddies.IntegrationTests.Group.Commands
{
    using TravelBuddies.Domain.Entities;
    using TravelBuddies.Application.Group.Commands.CreateGroup;
    using TravelBuddies.Application.Common.Exceptions.NotFound;

    public class CreateGroupHandlerTests : BaseHandlerTests
	{

		[Fact]
		public void CreateGroup_WithNonExistingCreator_ShouldThrowsException()
		{
			//Arrange
			var handler = new CreateGroupHandler(_repostiory, _userManager, _roleManager);
			var command = new CreateGroupCommand() { CreatorId = "1", PostId = 1 };

			//Assert
			Assert.ThrowsAsync<ApplicationUserNotFoundException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task CreateGroup_WithNonExistingPost_ShouldThrowsException()
		{
			//Arrange
			var handler = new CreateGroupHandler(_repostiory, _userManager, _roleManager);

			var user = new ApplicationUser() { UserName = "Test", Email = "email"};
			await _dbContext.AddAsync(user);
			await _dbContext.SaveChangesAsync();

			var command = new CreateGroupCommand() { CreatorId = user.Id, PostId = 10234 };

			//Assert
			await Assert.ThrowsAsync<PostNotFoundException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task CreateGroup_WithExistingCreatorAndPost_ShouldCreateAndReturnGroup()
		{
			//Arrange
			var handler = new CreateGroupHandler(_repostiory, _userManager, _roleManager);

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
			};

			await _dbContext.AddAsync(post);
			await _dbContext.SaveChangesAsync();

			var command = new CreateGroupCommand() 
			{ 
				CreatorId = user.Id, 
				PostId = post.Id 
			};

			//Act
			var result = await handler.Handle(command, default);

			//Assert
			Assert.NotNull(result);
			Assert.Equal(post.Id, result.PostId);
			Assert.Equal(post, result.Post);
			Assert.Equal(user.Id, result.CreatorId);
			Assert.Equal(user, result.Creator);
			Assert.Empty(result.UsersGroups);
			Assert.Empty(result.Messages);
		}
	}
}
