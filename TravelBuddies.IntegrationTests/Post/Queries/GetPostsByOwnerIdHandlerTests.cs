namespace TravelBuddies.IntegrationTests.Post.Queries
{
    using TravelBuddies.Application.Common.Exceptions.NotFound;
    using TravelBuddies.Application.Post.Queries.GetPostsByOwnerId;
    using TravelBuddies.Domain.Entities;

    public class GetPostsByOwnerIdHandlerTests : BaseHandlerTests
	{
		[Fact]
		public void GetPostsByOwnerId_WithNonExistingOwner_ShouldThrowsException()
		{
			//Arrange
			var handler = new GetPostsByOwnerIdHandler(_repostiory, _userManager, _roleManager);
			var query = new GetPostsByOwnerIdQuery("1");

			//Assert
			Assert.ThrowsAsync<ApplicationUserNotFoundException>(async () =>
			{
				await handler.Handle(query, default);
			});
		}

		[Fact]
		public async Task GetPostsByOwnerId_WithValidData_ShouldReturnPosts()
		{
			//Arrange
			var handler = new GetPostsByOwnerIdHandler(_repostiory, _userManager, _roleManager);

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

			var query = new GetPostsByOwnerIdQuery(user.Id);

			//Act
			var result = await handler.Handle(query, default);

			//Assert
			Assert.Equal(post, result.First());
		}
	}
}
