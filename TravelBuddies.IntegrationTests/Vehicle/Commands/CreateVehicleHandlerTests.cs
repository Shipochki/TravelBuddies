namespace TravelBuddies.IntegrationTests.Vehicle.Commands
{
    using TravelBuddies.Application.Common.Exceptions.NotFound;
    using TravelBuddies.Application.Common.Interfaces.AzureStorage;
    using TravelBuddies.Application.Vehicle.Commands.CreateVehicle;
    using TravelBuddies.Domain.Entities;
    using TravelBuddies.Domain.Enums;
    using TravelBuddies.IntegrationTests.Helpers;

    public class CreateVehicleHandlerTests : BaseHandlerTests
	{
		private readonly IBlobService _blobService;
        public CreateVehicleHandlerTests()
        {
			_blobService = new BlobServiceDummy();
        }

        [Fact]
		public void CreateVehicle_WithNonExistingUser_ShouldThrowsException()
		{
			//Arrange
			var handler = new CreateVehicleHandler(_repostiory, _userManager, _roleManager, _blobService);
			var command = new CreateVehicleCommand()
			{
				BrandName = "test",
				Color = "color",
				ModelName = "test",
				OwnerId = "1",
			};

			//Assert
			Assert.ThrowsAsync<ApplicationUserNotFoundException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task CreateVehicle_WithValidData_ShouldCreateVehicle()
		{
			//Arrange
			var handler = new CreateVehicleHandler(_repostiory, _userManager, _roleManager, _blobService);

			var user = new ApplicationUser() { UserName = "test", Email = "test" };

			await _dbContext.AddAsync(user);
			await _dbContext.SaveChangesAsync();

			var command = new CreateVehicleCommand()
			{
				BrandName = "test",
				Color = "color",
				ModelName = "test",
				OwnerId = user.Id,
				Fuel = (int)Fuel.Electric,
				SeatCount = 3,
				Year = 2024,
			};

			//Act
			var result = await handler.Handle(command, default);

			//Assert
			Assert.NotNull(result);
			Assert.Equal(user.Id, result.OwnerId);
			Assert.Equal(Fuel.Electric, result.Fuel);
			Assert.Equal(command.SeatCount, result.SeatCount);
			Assert.Equal(command.Year, result.Year);
			Assert.False(result.ACSystem);
			Assert.Equal("testLink", result.PictureLink);
		}
	}
}
