namespace TravelBuddies.Infrastructure
{
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore;
    using TravelBuddies.Domain.Entities;

    public class TravelBuddiesDbContext : IdentityDbContext<User>
    {
        public TravelBuddiesDbContext(DbContextOptions<TravelBuddiesDbContext> options)
            : base(options)
        {
            
        }

        public required DbSet<Review> Reviews { get; set; }

        public required DbSet<Message> Messages { get; set; }

        public required DbSet<Group> Groups { get; set; }

        public required DbSet<Post> Posts { get; set; }

        public required DbSet<Vehicle> Vehicles { get; set; }

        public required DbSet<UserGroup> UsersGroups { get; set; }

        public required DbSet<Log> Logs { get; set; }

        public required DbSet<City> Cities { get; set; }

        public required DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<IdentityRole> roles = new List<IdentityRole>()
            {
                { new IdentityRole() { Name = "Client" } },
                { new IdentityRole() { Name = "Driver" } },
                { new IdentityRole () { Name = "Admin" } }
            };

            modelBuilder.Entity<IdentityRole>()
                .HasData(roles);

            modelBuilder.Entity<UserGroup>()
                .HasKey(u => new { u.UserId, u.GroupId });

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
