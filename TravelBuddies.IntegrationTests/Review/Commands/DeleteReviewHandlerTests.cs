namespace TravelBuddies.IntegrationTests.Review.Commands
{
	using Microsoft.AspNetCore.Identity;
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Review.Commands.DeleteReview;
	using TravelBuddies.Domain.Common;
	using TravelBuddies.Domain.Entities;

	public class DeleteReviewHandlerTests : BaseHandlerTests
	{
		[Fact]
		public void DeleteReview_WithNonExistingReview_ShouldThrowsException()
		{
			//Arrange
			var handler = new DeleteReviewHandler(_repostiory, _userManager, _roleManager);
			var command = new DeleteReviewCommand(1, "1");

			//Assert
			Assert.ThrowsAsync<ReviewNotFoundException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task DeleteReview_WithNonExistingCreator_ShouldThrowsException()
		{
			//Arrange
			var handler = new DeleteReviewHandler(_repostiory, _userManager, _roleManager);

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

			var command = new DeleteReviewCommand(review.Id, "1");

			await Assert.ThrowsAsync<ApplicationUserNotFoundException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task DeleteReview_WithNonMatchingCreator_ShouldThrowsException()
		{
			//Arrange
			var handler = new DeleteReviewHandler(_repostiory, _userManager, _roleManager);

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

			var command = new DeleteReviewCommand(review.Id, user2.Id);

			await Assert.ThrowsAsync<ApplicationUserNotCreatorException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task DeleteReview_WithValidData_ShouldSetIsDeleteToTrue()
		{
			//Arrange
			var handler = new DeleteReviewHandler(_repostiory, _userManager, _roleManager);

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

			var command = new DeleteReviewCommand(review.Id, user1.Id);

			//Act
			await handler.Handle(command, default);

			//Assert
			Assert.True(review.IsDeleted);
		}
	}
}
