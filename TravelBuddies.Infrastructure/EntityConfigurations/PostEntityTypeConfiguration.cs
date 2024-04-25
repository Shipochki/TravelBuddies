namespace TravelBuddies.Infrastructure.EntityConfigurations
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using TravelBuddies.Domain.Entities;

	public class PostEntityTypeConfiguration : IEntityTypeConfiguration<Post>
	{
		public void Configure(EntityTypeBuilder<Post> builder)
		{
			builder
				.HasOne(p => p.FromDestinationCity)
				.WithMany()
				.HasForeignKey(p => p.FromDestinationCityId)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.HasOne(p => p.ToDestinationCity)
				.WithMany()
				.HasForeignKey(p => p.ToDestinationCityId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
