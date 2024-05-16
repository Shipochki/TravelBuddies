namespace TravelBuddies.IntegrationTests.Vehicle.Queries
{
    using TravelBuddies.Application.Common.Exceptions.NotFound;
    using TravelBuddies.Application.Vehicle.Queries.GetVehicleById;
    using TravelBuddies.Domain.Entities;

    public class GetVehicleByIdHandlerTests : BaseHandlerTests
	{
		[Fact]
		public void GetVehicleById_WithNonExistingVehicle_ShouldThrowsException()
		{
			//Arrange
			var handler = new GetVehicleByIdHandler(_repostiory, _userManager, _roleManager);
			var query = new GetVehicleByIdQuery(1);

			//Assert
			Assert.ThrowsAsync<VehicleNotFoundException>(async () =>
			{
				await handler.Handle(query, default);
			});
		}

		[Fact]
		public async Task GetVehicleById_WithValidVehicleId_ShouldVehicle()
		{
			//Arrange
			var handler = new GetVehicleByIdHandler(_repostiory, _userManager, _roleManager);

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

			var query = new GetVehicleByIdQuery(vehicle.Id);

			//Act
			var result = await handler.Handle(query, default);

			//Assert
			Assert.NotNull(result);
			Assert.Equal(result.Owner, user1);
			Assert.Equal(result.OwnerId, user1.Id);
		}
	}
}
