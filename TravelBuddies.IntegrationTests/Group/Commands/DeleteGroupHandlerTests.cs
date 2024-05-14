namespace TravelBuddies.IntegrationTests.Group.Commands
{
	using TravelBuddies.Domain.Entities;
	using TravelBuddies.Application.Group.Commands.DeleteGroup;
	using TravelBuddies.Application.Exceptions;

	public class DeleteGroupHandlerTests : BaseHandlerTests
	{
		[Fact]
		public void DeleteGroup_WithNonExistingGroup_ShouldThrowsException()
		{
			//Arrange
			var handler = new DeleteGroupHandler(_repostiory, _userManager, _roleManager);
			var command = new DeleteGroupCommand()
			{
				Id = 1,
				CreatorId = "1",
				PostId = 1,
			};

			//Assert
			Assert.ThrowsAsync<GroupNotFoundException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task DeleteGroup_WithNonExistingCreator_ShouldThrowsException()
		{
			//Arrange
			var handler = new DeleteGroupHandler(_repostiory, _userManager, _roleManager);
			var command = new DeleteGroupCommand()
			{
				Id = 1,
				CreatorId = "1",
				PostId = 1,
			};

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

			//Assert
			await Assert.ThrowsAsync<ApplicationUserNotFoundException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task DeleteGroup_WithNotMatchingCreator_ShouldThrowsException()
		{
			//Arrange
			var handler = new DeleteGroupHandler(_repostiory, _userManager, _roleManager);

			var user = new ApplicationUser { UserName = "testuser", Email = "test@example.com" };
			var user2 = new ApplicationUser { UserName = "test", Email = "test" };
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

			await _dbContext.AddAsync(user2);
			await _dbContext.AddAsync(group);
			await _dbContext.SaveChangesAsync();

			var command = new DeleteGroupCommand()
			{
				Id = group.Id,
				CreatorId = user2.Id,
				PostId = 1,
			};

			//Assert
			await Assert.ThrowsAsync<ApplicationUserNotCreatorException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task DeleteGroup_WithExistingEntities_ShouldSetIsDeletedToTrue()
		{
			//Arrange
			var handler = new DeleteGroupHandler(_repostiory, _userManager, _roleManager);

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

			var command = new DeleteGroupCommand()
			{
				Id = group.Id,
				CreatorId = user.Id,
				PostId = post.Id,
			};

			//Act
			await handler.Handle(command, default);

			//Assert
			Assert.True(group.IsDeleted);
			Assert.Equal(DateTime.Now.Date, group.DeletedOn.Date);
		}
	}
}
