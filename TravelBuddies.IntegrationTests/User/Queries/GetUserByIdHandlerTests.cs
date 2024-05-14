namespace TravelBuddies.IntegrationTests.User.Queries
{
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.User.Queries.GetUserById;
	using TravelBuddies.Domain.Entities;

	public class GetUserByIdHandlerTests : BaseHandlerTests
	{
		[Fact]
		public void GetUserById_WithNonExistingUser_ShouldThrowsException()
		{
			//Arrange
			var handler = new GetUserByIdHandler(_repostiory, _userManager, _roleManager);
			var query = new GetUserByIdQuery("1");

			//Assert
			Assert.ThrowsAsync<ApplicationUserNotFoundException>(async () =>
			{
				await handler.Handle(query, default);
			});
		}

		[Fact]
		public async Task GetUserById_WithValidData_ShouldReturnUser()
		{
			//Arrange
			var handler = new GetUserByIdHandler(_repostiory, _userManager, _roleManager);

			var user = new ApplicationUser()
			{
				FirstName = "firstname",
				LastName = "lastname",
				ProfilePictureLink = "profileLink",
				DriverLicenseFrontPictureLink = "driveFrontPic",
				DriverLicenseBackPictureLink = "driverBackPic",
				Country = "country",
				City = "city",
				CreatedOn = DateTime.Now,
				UserName = "test", 
				Email = "email" };

			await _dbContext.AddAsync(user);
			await _dbContext.SaveChangesAsync();

			var query = new GetUserByIdQuery(user.Id);

			//Act
			var result = await handler.Handle(query, default);

			//Assert
			Assert.Equal(user, result);
			Assert.Equal(user.FirstName, result.FirstName);
			Assert.Equal(user.LastName, result.LastName);
			Assert.Equal(user.ProfilePictureLink, result.ProfilePictureLink);
			Assert.Equal(user.DriverLicenseFrontPictureLink, result.DriverLicenseFrontPictureLink);
			Assert.Equal(user.DriverLicenseBackPictureLink, result.DriverLicenseBackPictureLink);
			Assert.Equal(user.Country, result.Country);
			Assert.Equal(user.City, result.City);
			Assert.Equal(user.CreatedOn, result.CreatedOn);
		}
	}
}
