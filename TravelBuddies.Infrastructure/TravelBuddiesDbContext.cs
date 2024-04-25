namespace TravelBuddies.Infrastructure
{
	using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore;
    using TravelBuddies.Domain.Entities;
	using TravelBuddies.Infrastructure.EntityConfigurations;

	public class TravelBuddiesDbContext : IdentityDbContext<ApplicationUser>
    {
        public TravelBuddiesDbContext(DbContextOptions<TravelBuddiesDbContext> options)
            : base(options)
        {
            
        }

		public DbSet<Review> Reviews { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<UserGroup> UsersGroups { get; set; }

        public DbSet<Log> Logs { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserGroupEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new GroupEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new ReviewEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new PostEntityTypeConfiguration());

            //modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }
    }
}
