namespace TravelBuddies.IntegrationTests.Vehicle.Queries
{
	using TravelBuddies.Application.Vehicle.Queries.GetVehicleByOwnerId;
	using TravelBuddies.Domain.Entities;

	public class GetVehicleByOwnerIdHandlerTests : BaseHandlerTests
	{
		[Fact]
		public async Task GetVehicleByOwnerId_WithNonExstingVehicle_ShouldReturnNull()
		{
			//Arrange
			var handler = new GetVehicleByOwnerIdHandler(_repostiory, _userManager, _roleManager);
			var query = new GetVehicleByOwnerIdQuery("1");

			//Act
			var result = await handler.Handle(query, default);

			//Assert
			Assert.Null(result);
		}

		[Fact]
		public async Task GetVehicleByOwnerId_WithExstingVehicle_ShouldReturnVehicle()
		{
			//Arrange
			var handler = new GetVehicleByOwnerIdHandler(_repostiory, _userManager, _roleManager);

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

			var query = new GetVehicleByOwnerIdQuery(user1.Id);

			//Act
			var result = await handler.Handle(query, default);

			//Assert
			Assert.NotNull(result);
			Assert.Equal(result, vehicle);
		}
	}
}
