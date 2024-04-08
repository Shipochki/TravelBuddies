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

		public DbSet<User> Users { get; set; }

		public DbSet<Role> Roles { get; set; }

		public DbSet<UserSubscription> UsersSubscriptions { get; set; }

		public DbSet<VerificationEmail> VerificationEmails { get; set; }

		public DbSet<Review> Reviews { get; set; }

		public DbSet<Message> Messages { get; set; }

		public DbSet<Group> Groups { get; set; }

		public DbSet<Post> Posts { get; set; }

		public DbSet<Vehicle> Vehicles { get; set; }

		public DbSet<UserGroup> UsersGroups { get; set; }

		public DbSet<Log> Logs { get; set; }

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

			modelBuilder.Entity<UserGroup>()
				.HasKey(u => new {u.UserId, u.GroupId});

			modelBuilder
				.Entity<UserGroup>()
				.HasOne(u => u.User)
				.WithMany(u => u.UsersGroups)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder
				.Entity<UserGroup>()
				.HasOne(u => u.Group)
				.WithMany(u => u.UsersGroups)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Group>()
				.HasOne(g => g.Creator)
				.WithMany(g => g.Groups)
				.HasForeignKey(g => g.CreatorId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Group>()
				.HasOne(g => g.Post)
				.WithOne(g => g.Group)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Review>()
				.HasOne(r => r.Creator)
				.WithMany(r => r.CreatedReviews)
				.HasForeignKey(r => r.CreatorId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Review>()
				.HasOne(r => r.Reciver)
				.WithMany(r => r.RecivedReviews)
				.HasForeignKey(r => r.ReciverId)
				.OnDelete(DeleteBehavior.Restrict);

			base.OnModelCreating(modelBuilder);
		}
	}
}
