namespace TravelBuddies.IntegrationTests.Post.Commands
{
    using TravelBuddies.Application.Common.Exceptions.NotFound;
    using TravelBuddies.Application.Post.Commands.UpdatePostGroup;
    using TravelBuddies.Domain.Entities;

    public class UpdatePostGroupHandlerTests : BaseHandlerTests
	{
		[Fact]
		public void UpdatePostGroup_WithNonExistingPost_ShouldThrowsException()
		{
			//Arrange
			var handler = new UpdatePostGroupHandler(_repostiory, _userManager, _roleManager);
			var command = new UpdatePostGroupCommand(1, 2);

			//Assert
			Assert.ThrowsAsync<PostNotFoundException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task UpdatePostGroup_WithNonExisgtinGroup_ShouldThrowsException()
		{
			//Arrange
			var handler = new UpdatePostGroupHandler(_repostiory, _userManager, _roleManager);

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

			var command = new UpdatePostGroupCommand(post.Id, 1);

			//Assert
			await Assert.ThrowsAsync<GroupNotFoundException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task UpdatePostGroup_WithValidData_ShouldUpdatePostGroup()
		{
			//Arrange
			var handler = new UpdatePostGroupHandler(_repostiory, _userManager, _roleManager);

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
			};

			await _dbContext.AddAsync(post);
			await _dbContext.AddAsync(group);
			await _dbContext.SaveChangesAsync();

			var command = new UpdatePostGroupCommand(post.Id, group.Id);

			//Act
			await handler.Handle(command, default);

			//Assert
			Assert.Equal(post.Group, group);
			Assert.Equal(post.GroupId, group.Id);
		}
	}
}
