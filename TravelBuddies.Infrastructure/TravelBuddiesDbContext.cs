namespace TravelBuddies.Infrastructure
{
	using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore;
    using TravelBuddies.Domain.Entities;

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
            modelBuilder.Entity<UserGroup>()
                .HasKey(u => new { u.UserId, u.GroupId });

            modelBuilder
                .Entity<UserGroup>()
                .HasOne(u => u.User)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<UserGroup>()
                .HasOne(u => u.Group)
                .WithMany(u => u.UsersGroups)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Group>()
                .HasOne(g => g.Creator)
                .WithMany()
                .HasForeignKey(g => g.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Group>()
                .HasOne(g => g.Post)
                .WithOne(g => g.Group)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Creator)
                .WithMany()
                .HasForeignKey(r => r.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Reciver)
                .WithMany()
                .HasForeignKey(r => r.ReciverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.FromDestinationCity)
                .WithMany()
                .HasForeignKey(p => p.FromDestinationCityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.ToDestinationCity)
                .WithMany()
                .HasForeignKey(p => p.ToDestinationCityId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
