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

		public BaseHandlerTests()
		{
			var options = new DbContextOptionsBuilder<TravelBuddiesDbContext>()
								.UseInMemoryDatabase(databaseName: "dbName")
								.Options;

			var context = new TravelBuddiesDbContext(options);

			_dbContext = context;

			_repostiory = new Repository(_dbContext);
			_userManager = new UserManager<ApplicationUser>(
			new UserStore<ApplicationUser>(_dbContext),
			null, null, null, null, null, null, null, null);
			_roleManager = Substitute.For<RoleManager<IdentityRole>>(
			Substitute.For<IRoleStore<IdentityRole>>(), null, null, null, null);
		}

	}
}
