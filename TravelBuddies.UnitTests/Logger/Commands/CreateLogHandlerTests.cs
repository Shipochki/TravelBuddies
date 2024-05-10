namespace TravelBuddies.UnitTests.Logger.Commands
{
	using TravelBuddies.Application.Logger.Commands.CreateLog;
	using TravelBuddies.Domain.Enums;

	public class CreateLogHandlerTests : BaseHandlerTests
	{
		[Fact]
		public async Task CreateLog_WithValidData_ShouldCreateLog()
		{
			//Arrange
			var handler = new CreateLogHandler(_repostiory, _userManager, _roleManager);
			var command = new CreateLogCommand("Test", LogLevel.Information);

			//Act
			await handler.Handle(command, default);

			//Assert
			Assert.Equal(1, _dbContext.Logs.Count());
		}
	}
}
