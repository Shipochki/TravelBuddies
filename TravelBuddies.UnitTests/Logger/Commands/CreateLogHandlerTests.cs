namespace TravelBuddies.UnitTests.Logger.Commands
{
	using Microsoft.EntityFrameworkCore;
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
			var result = await _dbContext.Logs.FirstOrDefaultAsync();

			//Assert
			Assert.Equal(1, _dbContext.Logs.Count());
			Assert.Equal(DateTime.Now.Date, result.CreatedOn.Date);
			Assert.Equal(command.Message, result.Message);
			Assert.Equal(command.Level, result.LogLevel);
		}
	}
}
