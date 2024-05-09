namespace TravelBuddies.UnitTests.Group.Commands
{
	using Microsoft.AspNetCore.Identity;
	using Microsoft.EntityFrameworkCore;
	using NSubstitute;
	using TravelBuddies.Domain.Entities;
	using TravelBuddies.Infrastructure.Repository;
	using TravelBuddies.Infrastructure;
	using TravelBuddies.Application.Group.Commands.CreateGroup;
	using TravelBuddies.Application.Exceptions;
	using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

	public class CreateGroupHandlerTests
	{
		private readonly Repository _repostiory;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public CreateGroupHandlerTests()
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
		public void CreateGroup_WithNonExistingCreator_ShouldThrownException()
		{
			//Arrange
			var handler = new CreateGroupHandler(_repostiory, _userManager, _roleManager);
			var command = new CreateGroupCommand() { CreatorId = "1", PostId = 1 };

			//Assert
			Assert.ThrowsAsync<ApplicationUserNotFoundException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task CreateGroup_WithNonExistingPost_ShouldThrownException()
		{
			//Arrange
			var handler = new CreateGroupHandler(_repostiory, _userManager, _roleManager);
			var command = new CreateGroupCommand() { CreatorId = "1", PostId = 1 };

			var user = new ApplicationUser { Id = "1" ,UserName = "testuser", Email = "test@example.com" };

			//Act
			await _repostiory.AddAsync(user);
			await _repostiory.SaveChangesAsync();

			//Assert
			await Assert.ThrowsAsync<PostNotFoundException>(async () =>
			{
				await handler.Handle(command, default);
			});

			_repostiory.Dispose();
		}

		[Fact]
		public async Task CreateGroup_WithExistingCreatorAndPost_ShouldCreateAndReturnGroup()
		{
			//Arrange
			var handler = new CreateGroupHandler(_repostiory, _userManager, _roleManager);

			var user = new ApplicationUser { UserName = "testuser", Email = "test@example.com" };
			var country = new Country() { Name = "country" };
			var city1 = new City() { Country = country, Name = "Sofia" };
			var city2 = new City() { Country = country, Name = "Targovishte" };
			var post = new Post 
			{ 
				Id = 1,
				Creator = user,
				CreatorId = "1", 
				Description = "test",
				FromDestinationCity = city1,
				ToDestinationCity = city2 
			};

			await _repostiory.AddAsync(post);
			await _repostiory.SaveChangesAsync();

			var command = new CreateGroupCommand() { CreatorId = user.Id, PostId = post.Id };


			//Act
			var result = await handler.Handle(command, default);

			//Assert
			Assert.NotNull(result);
			Assert.Equal(1, result.Id);
			Assert.Equal(post.Id, result.PostId);
			Assert.Equal(post, result.Post);
			Assert.Equal(user.Id, result.CreatorId);
			Assert.Equal(user, result.Creator);

			_repostiory.Dispose();
		}
	}
}
