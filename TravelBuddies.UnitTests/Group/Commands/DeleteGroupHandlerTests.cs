namespace TravelBuddies.UnitTests.Group.Commands
{
	using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.EntityFrameworkCore;
	using NSubstitute;
	using TravelBuddies.Domain.Entities;
	using TravelBuddies.Infrastructure.Repository;
	using TravelBuddies.Infrastructure;
	using TravelBuddies.Application.Group.Commands.DeleteGroup;
	using TravelBuddies.Application.Exceptions;

	public class DeleteGroupHandlerTests
	{
		private readonly Repository _repostiory;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public DeleteGroupHandlerTests()
		{
			var options = new DbContextOptionsBuilder<TravelBuddiesDbContext>()
				.UseInMemoryDatabase(databaseName: "dbName")
				.Options;

			var dbContext = new TravelBuddiesDbContext(options);

			_repostiory = new Repository(dbContext);
			_userManager = new UserManager<ApplicationUser>(
			new UserStore<ApplicationUser>(dbContext),
			null, null, null, null, null, null, null, null);
			_roleManager = Substitute.For<RoleManager<IdentityRole>>(
			Substitute.For<IRoleStore<IdentityRole>>(), null, null, null, null);
		}

		[Fact]
		public void DeleteGroup_WithNonExistingGroup_ShouldThrowException()
		{
			//Arrange
			var handler = new DeleteGroupHandler(_repostiory, _userManager, _roleManager);
			var command = new DeleteGroupCommand()
			{
				Id = 1,
				CreatorId = "1",
				PostId = 1,
			};

			//Assert
			Assert.ThrowsAsync<GroupNotFoundException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task DeleteGroup_WithNonExistingCreator_ShouldThrowException()
		{
			//Arrange
			var handler = new DeleteGroupHandler(_repostiory, _userManager, _roleManager);
			var command = new DeleteGroupCommand()
			{
				Id = 1,
				CreatorId = "1",
				PostId = 1,
			};

			var user = new ApplicationUser { UserName = "testuser", Email = "test@example.com" };
			var country = new Country() { Name = "country" };
			var city1 = new City() { Country = country, Name = "Sofia" };
			var city2 = new City() { Country = country, Name = "Targovishte" };
			var post = new Post
			{
				Creator = user,
				CreatorId = "1",
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

			await _repostiory.AddAsync(group);
			await _repostiory.SaveChangesAsync();

			//Assert
			await Assert.ThrowsAsync<ApplicationUserNotFoundException>(async () =>
			{
				await handler.Handle(command, default);
			});

			_repostiory.Dispose();
		}

		[Fact]
		public async Task DeleteGroup_WithNotMatchingCreator_ShouldThrowException()
		{
			//Arrange
			var handler = new DeleteGroupHandler(_repostiory, _userManager, _roleManager);
			
			var user = new ApplicationUser { UserName = "testuser", Email = "test@example.com" };
			var user2 = new ApplicationUser { UserName = "testTest", Email = "test@abv.bg" };
			var country = new Country() { Name = "country" };
			var city1 = new City() { Country = country, Name = "Sofia" };
			var city2 = new City() { Country = country, Name = "Targovishte" };
			var post = new Post
			{
				Creator = user,
				CreatorId = "1",
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

			await _repostiory.AddAsync(group);
			await _repostiory.AddAsync(user2);
			await _repostiory.SaveChangesAsync();
			
			var command = new DeleteGroupCommand()
			{
				Id = group.Id,
				CreatorId = user2.Id,
				PostId = 1,
			};

			//Assert
			await Assert.ThrowsAsync<ApplicationUserNotCreatorException>(async () =>
			{
				await handler.Handle(command, default);
			});

			_repostiory.Dispose();
		}

		[Fact]
		public async Task DeleteGroup_WithExistingEntities_ShouldSetIsDeletedToTrue()
		{
			//Arrange
			var handler = new DeleteGroupHandler(_repostiory, _userManager, _roleManager);

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

			await _repostiory.AddAsync(group);
			await _repostiory.SaveChangesAsync();
			
			var command = new DeleteGroupCommand()
			{
				Id = group.Id,
				CreatorId = user.Id,
				PostId = 1,
			};

			await handler.Handle(command, default);

			Assert.True(group.IsDeleted);

			_repostiory.Dispose();
		}
	}
}
