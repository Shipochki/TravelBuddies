namespace TravelBuddies.UnitTests.Post.Commands
{
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Post.Commands.CreatePost;
	using TravelBuddies.Application.Post.Commands.UpdatePost;
	using TravelBuddies.Domain.Entities;


	public class UpdatePostHandlerTests : BaseHandlerTests
	{
		[Fact]
		public void UpdatePost_WithNonExistingPost_ShouldThrowsException()
		{
			//Arrange
			var handler = new UpdatePostHandler(_repostiory, _userManager, _roleManager);
			var command = new UpdatePostCommand()
			{
				CreatorId = "1",
				DateAndTime = "",
				Description = "Description",
			};

			//Assert
			Assert.ThrowsAsync<PostNotFoundException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task UpdatePost_WithNonExistingFromDestination_ShouldThrowsException()
		{
			//Arrange
			var handler = new UpdatePostHandler(_repostiory, _userManager, _roleManager);

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

			var command = new UpdatePostCommand
			{
				CreatorId = user.Id,
				DateAndTime = "",
				Description = "Description",
				Id = post.Id,
			};

			//Assert
			await Assert.ThrowsAsync<CityNotFoundException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task UpdatePost_WithNonExistingToDestination_ShouldThrowsException()
		{
			//Arrange
			var handler = new UpdatePostHandler(_repostiory, _userManager, _roleManager);

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

			var command = new UpdatePostCommand()
			{
				CreatorId = user.Id,
				DateAndTime = "",
				Description = "Description",
				Id = post.Id,
				FromDestinationCityId = city1.Id,
			};

			//Assert
			await Assert.ThrowsAsync<CityNotFoundException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task UpdatePost_WithNotMatcingCreator_ShouldThrowsException()
		{
			//Arrange
			var handler = new UpdatePostHandler(_repostiory, _userManager, _roleManager);

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
			var user2 = new ApplicationUser { UserName = "test", Email = "email" };

			await _dbContext.AddAsync(user2);
			await _dbContext.AddAsync(post);
			await _dbContext.SaveChangesAsync();

			var command = new UpdatePostCommand()
			{
				CreatorId = user2.Id,
				DateAndTime = "",
				Description = "Description",
				Id = post.Id,
				FromDestinationCityId = city1.Id,
				ToDestinationCityId = city2.Id
			};

			//Assert
			await Assert.ThrowsAsync<ApplicationUserNotCreatorException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task UpdatePost_WithValidData_ShouldUpdatePost()
		{
			//Arrange
			var handler = new UpdatePostHandler(_repostiory, _userManager, _roleManager);

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

			var command = new UpdatePostCommand()
			{
				Id = post.Id,
				FromDestinationCityId = city1.Id,
				ToDestinationCityId = city2.Id,
				DateAndTime = DateTime.Now.ToString(),
				Description = "testUpdate",
				CreatorId = user.Id,
			};

			//Act
			await handler.Handle(command, default);

			//Assert
			Assert.Equal(post.Description, command.Description);
		}
	}
}
