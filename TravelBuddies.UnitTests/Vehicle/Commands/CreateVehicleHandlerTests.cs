﻿namespace TravelBuddies.UnitTests.Vehicle.Commands
{
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Interfaces.AzureStorage;
	using TravelBuddies.Application.Vehicle.Commands.CreateVehicle;
	using TravelBuddies.Domain.Entities;
	using TravelBuddies.UnitTests.Helpers;

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
				OwnerId = user.Id
			};

			//Act
			var result = await handler.Handle(command, default);

			//Assert
			Assert.NotNull(result);
			Assert.Equal(user.Id, result.OwnerId);
		}
	}
}
