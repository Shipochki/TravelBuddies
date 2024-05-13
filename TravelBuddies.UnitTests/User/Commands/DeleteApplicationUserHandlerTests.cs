namespace TravelBuddies.UnitTests.User.Commands
{
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.User.Commands.DeleteApplicationUser;
	using TravelBuddies.Domain.Entities;

	public class DeleteApplicationUserHandlerTests : BaseHandlerTests
	{
		[Fact]
		public void DeleteApplicationUser_WithNonExistingUser_ShouldThrowsException()
		{
			//Arrange
			var handler = new DeleteApplicationUserHandler(_repostiory, _userManager, _roleManager);
			var command = new DeleteApplicationUserCommand("1");

			//Assert
			Assert.ThrowsAsync<ApplicationUserNotFoundException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task DeleteApplicationUser_WithValidData_ShouldSetIsDeletedToTrue()
		{
			//Arrange
			var handler = new DeleteApplicationUserHandler(_repostiory, _userManager, _roleManager);
			var user = new ApplicationUser() { UserName = "test", Email = "email" };

			await _dbContext.AddAsync(user);
			await _dbContext.SaveChangesAsync();

			var command = new DeleteApplicationUserCommand(user.Id);

			//Act
			await handler.Handle(command, default);

			//Assert
			Assert.True(user.IsDeleted);
			Assert.Equal(user.DeletedOn.Date, DateTime.Now.Date);
		}
	}
}
