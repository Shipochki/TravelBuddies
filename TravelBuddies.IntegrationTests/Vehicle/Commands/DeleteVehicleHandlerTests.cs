namespace TravelBuddies.IntegrationTests.Vehicle.Commands
{
    using Microsoft.EntityFrameworkCore;
    using TravelBuddies.Application.Common.Exceptions.Forbidden;
    using TravelBuddies.Application.Common.Exceptions.NotFound;
    using TravelBuddies.Application.Vehicle.Commands.DeleteVehicle;
    using TravelBuddies.Domain.Entities;

    public class DeleteVehicleHandlerTests : BaseHandlerTests
	{
		[Fact]
		public void DeleteVehicle_WithNonExistingVehicle_ShouldThrowsException()
		{
			//Arrange
			var handler = new DeleteVehicleHandler(_repostiory, _userManager, _roleManager);
			var command = new DeleteVehicleCommand(1, "1");

			//Assert
			Assert.ThrowsAsync<VehicleNotFoundException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task DeleteVehicle_WithNonExistingOwner_ShouldThrowsException()
		{
			//Arrange
			var handler = new DeleteVehicleHandler(_repostiory, _userManager, _roleManager);

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

			var command = new DeleteVehicleCommand(vehicle.Id, "1");

			//Assert
			await Assert.ThrowsAsync<ApplicationUserNotFoundException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task DeleteVehicle_WithNonMachingOwner_ShouldThrowsException()
		{
			//Arrange
			var handler = new DeleteVehicleHandler(_repostiory, _userManager, _roleManager);

			var user1 = new ApplicationUser() { UserName = "test", Email = "email" };
			var user2 = new ApplicationUser() { UserName = "test2", Email = "email2" };
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
			await _dbContext.AddAsync(user2);
			await _dbContext.SaveChangesAsync();

			var command = new DeleteVehicleCommand(vehicle.Id, user2.Id);

			//Assert
			await Assert.ThrowsAsync<ApplicationUserNotCreatorException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task DeleteVehicle_WithValidData_ShouldDeleteVehicle()
		{
			//Arrange
			var handler = new DeleteVehicleHandler(_repostiory, _userManager, _roleManager);

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

			var command = new DeleteVehicleCommand(vehicle.Id, user1.Id);

			//Act
			await handler.Handle(command, default);
			var result = await _dbContext.Vehicles.FirstOrDefaultAsync();

			//Assert
			Assert.Null(result);
		}
	}
}
