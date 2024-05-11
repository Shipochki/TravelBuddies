namespace TravelBuddies.UnitTests.Post.Queries
{
	using TravelBuddies.Application.Post.Queries.GetPostsBySearch;
	using TravelBuddies.Domain.Entities;

	public class GetPostsBySearchHandlerTests : BaseHandlerTests
	{
		[Fact]
		public async Task GetPostsBySearch_WithOnlyCities_ShouldReturnPosts()
		{
			//Arrange
			var handler = new GetPostBySearchHandler(_repostiory, _userManager, _roleManager);

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

			var query = new GetPostBySearchQuery()
			{
				FromDestinationCityId = city1.Id,
				ToDestinationCityId = city2.Id,
			};

			//Act
			var result = await handler.Handle(query, default);

			//Assert
			Assert.Equal(post, result.First());
		}
	}
}
