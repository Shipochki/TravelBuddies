namespace TravelBuddies.IntegrationTests.Vehicle.Commands
{
	using Microsoft.EntityFrameworkCore;
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Interfaces.AzureStorage;
	using TravelBuddies.Application.Vehicle.Commands.UpdateVehicle;
	using TravelBuddies.Domain.Entities;
	using TravelBuddies.IntegrationTests.Helpers;

	public class UpdateVehicleHandlerTests : BaseHandlerTests
	{
		private readonly IBlobService _blobService;	

        public UpdateVehicleHandlerTests()
        {
			_blobService = new BlobServiceDummy();
        }

        [Fact]
		public void UpdateVehicle_WithNonExistingVehicle_ShouldThrowsException()
		{
			//Arrange
			var handler = new UpdateVehicleHandler(_repostiory, _userManager, _roleManager, _blobService);
			var command = new UpdateVehicleCommand()
			{
				BrandName = "test",
				ModelName = "test",
				Color = "color",
				OwnerId = "1",
			};

			//Assert
			Assert.ThrowsAsync<VehicleNotFoundException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task UpdateVehicle_WithNonMatchingOwner_ShouldThrowsException()
		{
			//Arrange
			var handler = new UpdateVehicleHandler(_repostiory, _userManager, _roleManager, _blobService);

			var user1 = new ApplicationUser() { UserName = "test", Email = "email" };
			var vehicle = new Vehicle()
			{
				BrandName = "test",
				ModelName = "test",
				Color = "color",
				PictureLink = "test",
				Owner = user1,
				OwnerId = user1.Id,
			};

			await _dbContext.AddAsync(vehicle);
			await _dbContext.SaveChangesAsync();

			var command = new UpdateVehicleCommand()
			{
				Id = vehicle.Id,
				BrandName = "test1",
				ModelName = "test1",
				Color = "color",
				OwnerId = "1",
			};

			//Assert
			await Assert.ThrowsAsync<ApplicationUserNotCreatorException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task UpdateVehicle_WithValidData_ShouldUpdateVehicle()
		{
			//Arrange
			var handler = new UpdateVehicleHandler(_repostiory, _userManager, _roleManager, _blobService);

			var user1 = new ApplicationUser() { UserName = "test", Email = "email" };
			var vehicle = new Vehicle()
			{
				BrandName = "test",
				ModelName = "test",
				Color = "color",
				PictureLink = "test",
				Owner = user1,
				OwnerId = user1.Id,
			};

			await _dbContext.AddAsync(vehicle);
			await _dbContext.SaveChangesAsync();

			var command = new UpdateVehicleCommand()
			{
				Id = vehicle.Id,
				BrandName = "test1",
				ModelName = "test1",
				Color = "color1",
				OwnerId = user1.Id,
			};

			//Act
			await handler.Handle(command, default);

			//Assert
			Assert.Equal(vehicle.BrandName, command.BrandName);
			Assert.Equal(vehicle.ModelName, command.ModelName);
			Assert.Equal(vehicle.Color, command.Color);
		}
	}
}
