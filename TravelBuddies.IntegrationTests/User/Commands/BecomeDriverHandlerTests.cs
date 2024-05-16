namespace TravelBuddies.IntegrationTests.User.Commands
{
    using Microsoft.AspNetCore.Identity;
    using NSubstitute;
    using TravelBuddies.Application.Common.Exceptions.NotFound;
    using TravelBuddies.Application.User.Commands.BecomeDriver;
    using TravelBuddies.Domain.Common;
    using TravelBuddies.Domain.Entities;

    public class BecomeDriverHandlerTests : BaseHandlerTests
	{
		[Fact]
		public void BecomeDriver_WithNonExistingUser_ShouldThrowsException()
		{
			//Arrange
			var handler = new BecomeDriverHandler(_repostiory, _userManager, _roleManager);
			var command = new BecomeDriverCommand("1");

			//Assert
			Assert.ThrowsAsync<ApplicationUserNotFoundException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task BecomeDriver_WithNonExistingRole_ShouldThrowsExcpetion()
		{
			//Arrange
			var handler = new BecomeDriverHandler(_repostiory, _userManager, _roleManager);

			var user = new ApplicationUser() { UserName = "test", Email = "email" };

			await _dbContext.AddAsync(user);
			await _dbContext.SaveChangesAsync();

			var command = new BecomeDriverCommand(user.Id);

			//Assert
			await Assert.ThrowsAsync<IdentityRoleNotFoundException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task BecomeDriver_WithSuccesfulAddRole_ShouldAddRoleToUser()
		{
			//Arrange
			var handler = new BecomeDriverHandler(_repostiory, _userManager, _roleManager);

			var user = new ApplicationUser() { UserName = "test", Email = "email" };
			var role = new IdentityRole() {
				Id = "e3561acf-d05f-494a-95b0-7e12433ec036",
				Name = "driver",
				NormalizedName = "DRIVER"
			};

			await _roleManager.CreateAsync(role);
			await _dbContext.AddAsync(user);
			await _dbContext.SaveChangesAsync();

			var command = new BecomeDriverCommand(user.Id);

			//Act
			await handler.Handle(command, default);

			//Assert
			Assert.True(await _userManager.IsInRoleAsync(user, ApplicationRoles.Driver));
		}
	}
}
