namespace TravelBuddies.UnitTests.Review.Commands
{
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Review.Commands.UpdateReview;
	using TravelBuddies.Domain.Entities;

	public class UpdateReviewHandlerTests : BaseHandlerTests
	{
		[Fact]
		public void UpdateReview_WithNonExistingReview_ShouldThrowsException()
		{
			//Arrange
			var handler = new UpdateReviewHandler(_repostiory, _userManager, _roleManager);
			var command = new UpdateReviewCommand()
			{
				CreatorId = "1",
				ReciverId = "1",
			};

			//Assert
			Assert.ThrowsAsync<ReviewNotFoundException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task UpdateReview_WithNonMatchingCreator_ShouldThrowsException()
		{
			//Arrange
			var handler = new UpdateReviewHandler(_repostiory, _userManager, _roleManager);

			var user1 = new ApplicationUser() { UserName = "Test1", Email = "email1" };
			var user2 = new ApplicationUser() { UserName = "Test2", Email = "email2" };
			var review = new Review()
			{
				Creator = user1,
				CreatorId = user1.Id,
				Reciver = user2,
				ReciverId = user2.Id,
			};

			await _dbContext.AddAsync(review);
			await _dbContext.SaveChangesAsync();

			var command = new UpdateReviewCommand()
			{
				Id = review.Id,
				CreatorId = "1",
				ReciverId = "1",
			};

			//Assert
			await Assert.ThrowsAsync<ApplicationUserNotCreatorException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task UpdateReview_WithValidData_ShouldUpdateReview()
		{
			//Arrange
			var handler = new UpdateReviewHandler(_repostiory, _userManager, _roleManager);

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

			var command = new UpdateReviewCommand()
			{
				Id = review.Id,
				CreatorId = user1.Id,
				ReciverId = user2.Id,
				Text = "testUpdate",
				Rating = 5,
			};

			//Act
			var result = await handler.Handle(command, default);

			//Assert
			Assert.Equal(command.Text, result.Text);
			Assert.Equal(command.Rating, result.Rating);
		}
	}
}
