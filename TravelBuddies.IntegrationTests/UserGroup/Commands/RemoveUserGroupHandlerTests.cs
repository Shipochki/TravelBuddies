namespace TravelBuddies.IntegrationTests.UserGroup.Commands
{
	using Microsoft.EntityFrameworkCore;
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.UserGroup.Commands.RemoveUserGroup;
	using TravelBuddies.Domain.Entities;

	public class RemoveUserGroupHandlerTests : BaseHandlerTests
	{
		[Fact]
		public void RemoveUserGroup_WithNonExistingUserForRemove_ShouldThrowsException()
		{
			//Arrange
			var handler = new RemoveUserGroupHandler(_repostiory, _userManager, _roleManager);
			var command = new RemoveUserGroupCommand()
			{
				OwnerId = "1",
				UserIdForRemove = "1",
			};

			//Assert
			Assert.ThrowsAsync<ApplicationUserNotFoundException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task RemoveUserGroup_WithNonExistingOwner_ShouldThrowsException()
		{
			//Arrange
			var handler = new RemoveUserGroupHandler(_repostiory, _userManager, _roleManager);

			var user1 = new ApplicationUser() { UserName = "test1", Email = "email1" };

			await _dbContext.AddAsync(user1);
			await _dbContext.SaveChangesAsync();

			var command = new RemoveUserGroupCommand()
			{
				UserIdForRemove = user1.Id,
				OwnerId = "1",
			};

			//Assert
			await Assert.ThrowsAsync<ApplicationUserNotFoundException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task RemoveUserGroup_WithNonExistingGroup_ShouldThrowsException()
		{
			//Arrange
			var handler = new RemoveUserGroupHandler(_repostiory, _userManager, _roleManager);

			var user1 = new ApplicationUser { UserName = "testuser", Email = "test@example.com" };
			var user2 = new ApplicationUser { UserName = "test", Email = "email" };

			await _dbContext.AddRangeAsync(user1, user2);
			await _dbContext.SaveChangesAsync();

			var command = new RemoveUserGroupCommand()
			{
				OwnerId = user1.Id,
				GroupId = 1,
				UserIdForRemove = user2.Id
			};

			//Assert
			await Assert.ThrowsAsync<GroupNotFoundException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task RemoveUserGroup_WithNonMatchingOwner_ShouldThrowsException()
		{
			//Arrange
			var handler = new RemoveUserGroupHandler(_repostiory, _userManager, _roleManager);

			var user1 = new ApplicationUser { UserName = "testuser", Email = "test@example.com" };
			var user2 = new ApplicationUser { UserName = "test", Email = "email" };
			var country = new Country() { Name = "country" };
			var city1 = new City() { Country = country, Name = "Sofia" };
			var city2 = new City() { Country = country, Name = "Targovishte" };
			var post = new Post
			{
				Creator = user1,
				CreatorId = user1.Id,
				Description = "test",
				FromDestinationCity = city1,
				ToDestinationCity = city2
			};
			var group = new Group()
			{
				Creator = user1,
				CreatorId = user1.Id,
				Post = post,
				PostId = post.Id,
				CreatedOn = DateTime.UtcNow,
			};

			await _dbContext.AddAsync(group);
			await _dbContext.AddAsync(user2);
			await _dbContext.SaveChangesAsync();

			var command = new RemoveUserGroupCommand()
			{
				GroupId = group.Id,
				OwnerId = user2.Id,
				UserIdForRemove = user1.Id,
			};

			//Assert
			await Assert.ThrowsAsync<ApplicationUserNotCreatorException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task RemoveUserGroup_WithNonExistingUserGroup_ShouldThrowsException()
		{
			//Arrange
			var handler = new RemoveUserGroupHandler(_repostiory, _userManager, _roleManager);

			var user1 = new ApplicationUser { UserName = "testuser", Email = "test@example.com" };
			var user2 = new ApplicationUser { UserName = "test", Email = "email" };
			var country = new Country() { Name = "country" };
			var city1 = new City() { Country = country, Name = "Sofia" };
			var city2 = new City() { Country = country, Name = "Targovishte" };
			var post = new Post
			{
				Creator = user1,
				CreatorId = user1.Id,
				Description = "test",
				FromDestinationCity = city1,
				ToDestinationCity = city2
			};
			var group = new Group()
			{
				Creator = user1,
				CreatorId = user1.Id,
				Post = post,
				PostId = post.Id,
				CreatedOn = DateTime.UtcNow,
			};

			await _dbContext.AddAsync(group);
			await _dbContext.AddAsync(user2);
			await _dbContext.SaveChangesAsync();

			var command = new RemoveUserGroupCommand()
			{
				GroupId = group.Id,
				OwnerId = user1.Id,
				UserIdForRemove = user2.Id,
			};

			//Assert
			await Assert.ThrowsAsync<ApplicationUserNotInGroupException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task RemoveUserGroup_WithValidData_ShouldDeleteUserGroup()
		{
			//Arrange
			var handler = new RemoveUserGroupHandler(_repostiory, _userManager, _roleManager);

			var user1 = new ApplicationUser { UserName = "testuser", Email = "test@example.com" };
			var user2 = new ApplicationUser { UserName = "test", Email = "email" };
			var country = new Country() { Name = "country" };
			var city1 = new City() { Country = country, Name = "Sofia" };
			var city2 = new City() { Country = country, Name = "Targovishte" };
			var post = new Post
			{
				Creator = user1,
				CreatorId = user1.Id,
				Description = "test",
				FromDestinationCity = city1,
				ToDestinationCity = city2
			};
			var group = new Group()
			{
				Creator = user1,
				CreatorId = user1.Id,
				Post = post,
				PostId = post.Id,
				CreatedOn = DateTime.UtcNow,
			};
			var userGroup = new UserGroup()
			{
				User = user2,
				UserId = user2.Id,
				Group = group,
				GroupId = group.Id,
			};

			await _dbContext.AddAsync(userGroup);
			await _dbContext.AddAsync(user2);
			await _dbContext.SaveChangesAsync();

			var command = new RemoveUserGroupCommand()
			{
				GroupId = group.Id,
				OwnerId = user1.Id,
				UserIdForRemove = user2.Id,
			};

			//Act
			await handler.Handle(command, default);
			var result = await _dbContext.UsersGroups.FirstOrDefaultAsync();

			//Assert
			Assert.Null(result);
		}
	}
}
