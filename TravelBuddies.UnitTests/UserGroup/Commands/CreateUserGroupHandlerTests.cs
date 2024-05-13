namespace TravelBuddies.UnitTests.UserGroup.Commands
{
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.UserGroup.Commands.CreateUserGroup;
	using TravelBuddies.Domain.Entities;

	public class CreateUserGroupHandlerTests : BaseHandlerTests
	{
		[Fact]
		public void CreateUserGroup_WithNonEsxistingUser_ShouldThrowsException()
		{
			//Arrange
			var handler = new CreateUserGroupHandler(_repostiory, _userManager, _roleManager);
			var command = new CreateUserGroupCommand() { UserId = "1" };

			//Assert
			Assert.ThrowsAsync<ApplicationUserNotFoundException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task CreateUserGroup_WithNonExisitngGroup_ShouldThrowsException()
		{
			//Arrange
			var handler = new CreateUserGroupHandler(_repostiory, _userManager, _roleManager);

			var user = new ApplicationUser() { UserName = "test", Email = "test" };

			await _dbContext.AddAsync(user);
			await _dbContext.SaveChangesAsync();

			var command = new CreateUserGroupCommand()
			{
				UserId = user.Id
			};

			//Assert
			await Assert.ThrowsAsync<GroupNotFoundException>(async () =>
			{
				await handler.Handle(command, default); 
			});
		}

		[Fact]
		public async Task CreateUserGroup_WithExisitngUserGroup_ShouldThrowsException()
		{
			//Arrange
			var handler = new CreateUserGroupHandler(_repostiory, _userManager, _roleManager);

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

			var command = new CreateUserGroupCommand()
			{
				GroupId = group.Id,
				UserId = user.Id
			};

			//Assert
			await Assert.ThrowsAsync<ApplicationUserAllreadyInGroupException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task CreateUserGroup_WithNotFreeSeats_ShouldThrowsException()
		{
			//Arrange
			var handler = new CreateUserGroupHandler(_repostiory, _userManager, _roleManager);

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
				ToDestinationCity = city2,
				FreeSeats = 0
			};
			var group = new Group()
			{
				Creator = user,
				CreatorId = user.Id,
				Post = post,
				PostId = post.Id,
				CreatedOn = DateTime.UtcNow,
			};

			await _dbContext.AddAsync(group);
			await _dbContext.SaveChangesAsync();

			var command = new CreateUserGroupCommand()
			{
				GroupId = group.Id,
				UserId = user.Id
			};

			//Assert
			await Assert.ThrowsAsync<NotAvailableSeatsInPostException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task CreateUserGroup_WithValidData_ShouldCreateUserGroup()
		{
			//Arrange
			var handler = new CreateUserGroupHandler(_repostiory, _userManager, _roleManager);

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
				ToDestinationCity = city2,
				FreeSeats = 3
			};
			var group = new Group()
			{
				Creator = user,
				CreatorId = user.Id,
				Post = post,
				PostId = post.Id,
				CreatedOn = DateTime.UtcNow,
			};

			await _dbContext.AddAsync(group);
			await _dbContext.SaveChangesAsync();

			var command = new CreateUserGroupCommand()
			{
				GroupId = group.Id,
				UserId = user.Id
			};

			//Act
			await handler.Handle(command, default);
			var userGroup = _dbContext.UsersGroups.First();

			//Assert
			Assert.Equal(user.Id, userGroup.UserId);
			Assert.Equal(user, userGroup.User);
			Assert.Equal(group.Id, userGroup.GroupId);
			Assert.Equal(group, userGroup.Group);
		}
	}
}
