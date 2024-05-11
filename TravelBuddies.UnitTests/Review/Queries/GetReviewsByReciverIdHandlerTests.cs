namespace TravelBuddies.UnitTests.Review.Queries
{
	using TravelBuddies.Application.Review.Queries.GetReviewsByReciverId;
	using TravelBuddies.Domain.Entities;

	public class GetReviewsByReciverIdHandlerTests : BaseHandlerTests
	{
		[Fact]
		public async Task GetReviewsByReciverId_ShouldReturnReview()
		{
			//Arrange
			var handler = new GetReviewsByReciverIdHandler(_repostiory, _userManager, _roleManager);

			var user1 = new ApplicationUser() { UserName = "Test1", Email = "email1" };
			var user2 = new ApplicationUser() { UserName = "Test2", Email = "email2" };
			var review = new Review()
			{
				Creator = user1,
				CreatorId = user1.Id,
				Reciver = user2,
				ReciverId = user2.Id,
				Text = "test",
				Rating = 4
			};

			await _dbContext.AddAsync(review);
			await _dbContext.SaveChangesAsync();

			var query = new GetReviewsByReciverIdQuery(user2.Id);

			//Act
			var result = await handler.Handle(query, default);

			//Assert
			Assert.Equal(review, result.First());
		}
	}
}
