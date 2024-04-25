namespace TravelBuddies.Infrastructure.EntityConfigurations
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using TravelBuddies.Domain.Entities;

	public class UserGroupEntityTypeConfiguration : IEntityTypeConfiguration<UserGroup>
	{
		public void Configure(EntityTypeBuilder<UserGroup> builder)
		{
			builder
				.HasKey(u => new { u.UserId, u.GroupId });

			builder
				.HasOne(u => u.User)
				.WithMany()
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.HasOne(u => u.Group)
				.WithMany(u => u.UsersGroups)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
