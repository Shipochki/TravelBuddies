namespace TravelBuddies.IntegrationTests.UserGroup.Commands
{
    using Microsoft.EntityFrameworkCore;
    using TravelBuddies.Application.Common.Exceptions.NotFound;
    using TravelBuddies.Application.UserGroup.Commands.DeleteUserGroup;
    using TravelBuddies.Domain.Entities;

    public class DeleteUserGroupHandlerTests : BaseHandlerTests
	{
		[Fact]
		public void DeleteUserGroup_WithUserNotInGroup_ShouldThrowsException()
		{
			//Arrange
			var handler = new DeleteUserGroupHandler(_repostiory, _userManager, _roleManager);
			var command = new DeleteUserGroupCommand() { UserId = "1" };

			//Assert
			Assert.ThrowsAsync<ApplicationUserNotInGroupException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task DeleteUserGroup_WithValidData_ShouldDeleteUserGroup()
		{
			//Arrange
			var handler = new DeleteUserGroupHandler(_repostiory, _userManager, _roleManager);

			var user = new ApplicationUser { UserName = "testuser", Email = "test@example.com" };
			var country = new Country() { Name = "country" };
			var city1 = new City() { Country = country, Name = "Sofia" };
			var city2 = new City() { Country = country, Name = "Targovishte" };
			var post = new Post
			{
				Creator = user,
				CreatorId = user.Id,
				Description = "test",
				FromDestinationCity = city1,
				ToDestinationCity = city2
			};
			var group = new Group()
			{
				Creator = user,
				CreatorId = user.Id,
				Post = post,
				PostId = post.Id,
				CreatedOn = DateTime.UtcNow,
			};
			var userGroup = new UserGroup()
			{
				Group = group,
				User = user,
				UserId = user.Id,
			};

			await _dbContext.AddAsync(userGroup);
			await _dbContext.SaveChangesAsync();

			var command = new DeleteUserGroupCommand()
			{
				UserId = user.Id,
				GroupId = group.Id,
			};

			//Act
			await handler.Handle(command, default);
			var result = await _dbContext.UsersGroups.FirstOrDefaultAsync();

			//Assert
			Assert.Null(result);
		}
	}
}
