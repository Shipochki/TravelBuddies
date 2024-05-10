namespace TravelBuddies.UnitTests
{
	using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
	using Microsoft.AspNetCore.Identity;
	using NSubstitute;
	using TravelBuddies.Domain.Entities;
	using TravelBuddies.Infrastructure.Repository;
	using TravelBuddies.Infrastructure;
	using Microsoft.EntityFrameworkCore;

	public class BaseHandlerTests
	{
		protected readonly Repository _repostiory;
		protected readonly UserManager<ApplicationUser> _userManager;
		protected readonly RoleManager<IdentityRole> _roleManager;
		protected TravelBuddiesDbContext _dbContext;
		private readonly DbContextOptions<TravelBuddiesDbContext> _options;

		public BaseHandlerTests()
		{
			_options = new DbContextOptionsBuilder<TravelBuddiesDbContext>()
								.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
								.Options;

			var context = new TravelBuddiesDbContext(_options);

			_dbContext = context;

			_repostiory = new Repository(_dbContext);
			_userManager = new UserManager<ApplicationUser>(
			new UserStore<ApplicationUser>(_dbContext),
			null, null, null, null, null, null, null, null);
			_roleManager = Substitute.For<RoleManager<IdentityRole>>(
			Substitute.For<IRoleStore<IdentityRole>>(), null, null, null, null);
		}

		protected void Dispose()
		{
			// Clear your database
			using (_dbContext = new TravelBuddiesDbContext(_options))
			{
				_dbContext.Database.EnsureDeleted();
			}
		}

	}
}
