namespace TravelBuddies.Infrastructure.Database
{
	using Microsoft.EntityFrameworkCore;
	using TravelBuddies.Domain.Entities;

	public class TravelBuddiesDbContext : DbContext
	{
		public TravelBuddiesDbContext(DbContextOptions<TravelBuddiesDbContext> options)
			: base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			List<Role> roles = new List<Role>()
			{
				{ new Role() { Id = 1, Name = "Client" } },
				{ new Role() { Id = 2, Name = "Driver"} },
				{ new Role() { Id = 3, Name = "Admin"} }
			};

			modelBuilder.Entity<Role>()
				.HasData(roles);

			base.OnModelCreating(modelBuilder);
		}
	}
}
