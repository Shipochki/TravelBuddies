namespace TravelBuddies.IntegrationTests.Review.Commands
{
    using TravelBuddies.Application.Common.Exceptions;
    using TravelBuddies.Application.Review.Commands.CreateReview;
    using TravelBuddies.Domain.Entities;

    public class CreateReviewHandlerTests : BaseHandlerTests
	{
		[Fact]
		public void CreateReview_WithNonExistingCreator_ShouldThrowsException()
		{
			//Arrange
			var handler = new CreateReviewHandler(_repostiory, _userManager, _roleManager);
			var command = new CreateReviewCommand(){CreatorId = "1", ReciverId = "2" };

			//Assert
			Assert.ThrowsAsync<ApplicationUserNotFoundException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task CreateReview_WithNonExistingReciver_ShouldThrowsException()
		{
			//Arrange
			var handler = new CreateReviewHandler(_repostiory, _userManager, _roleManager);
			
			var user = new ApplicationUser() { UserName = "Test", Email = "email"};

			await _dbContext.AddAsync(user);
			await _dbContext.SaveChangesAsync();

			var command = new CreateReviewCommand() { CreatorId = user.Id, ReciverId = "1" };

			//Assert
			await Assert.ThrowsAsync<ApplicationUserNotFoundException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task CreateReview_WithValidData_ShouldCreateReview()
		{
			//Arrange
			var handler = new CreateReviewHandler(_repostiory, _userManager, _roleManager);

			var user1 = new ApplicationUser() { UserName = "Test1", Email = "email1" };
			var user2 = new ApplicationUser() { UserName = "Test2", Email = "email2" };

			await _dbContext.AddRangeAsync(user1, user2);
			await _dbContext.SaveChangesAsync();

			var command = new CreateReviewCommand()
			{
				CreatorId = user1.Id,
				ReciverId = user2.Id,
				Text = "test",
				Rating = 4,
			};

			//Act
			var result = await handler.Handle(command, default);

			//Assert
			Assert.Equal(user1.Id, result.CreatorId);
			Assert.Equal(user1, result.Creator);
			Assert.Equal(user2.Id, result.ReciverId);
			Assert.Equal(user2, result.Reciver);
			Assert.Equal("test", result.Text);
			Assert.Equal(4, result.Rating);
		}
	}
}
