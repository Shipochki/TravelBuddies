namespace TravelBuddies.IntegrationTests.Post.Commands
{
    using TravelBuddies.Application.Common.Exceptions.Forbidden;
    using TravelBuddies.Application.Common.Exceptions.NotFound;
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
				Currency = "eur"
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
				ToDestinationCity = city2,
				Currency = "eur"
			};

			await _dbContext.AddAsync(post);
			await _dbContext.SaveChangesAsync();

			var command = new UpdatePostCommand
			{
				CreatorId = user.Id,
				DateAndTime = "",
				Description = "Description",
				Id = post.Id,
				Currency = "tes"
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
				ToDestinationCity = city2,
				Currency = "EUR"
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
				Currency = "tes"
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
				ToDestinationCity = city2,
				Currency = "eur"
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
				ToDestinationCityId = city2.Id,
				Currency = "tes"
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
				ToDestinationCity = city2,
				Currency = "eur"
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
				Currency = "tes"
			};

			//Act
			await handler.Handle(command, default);

			//Assert
			Assert.Equal(post.Description, command.Description);
			Assert.Equal(DateTime.Now.Date, post.UpdatedOn.Value.Date);
		}
	}
}
