namespace TravelBuddies.Infrastructure.EntityConfigurations
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using TravelBuddies.Domain.Entities;
	using static TravelBuddies.Domain.Common.DataConstants.PostConstants;

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

			builder
				.Property(p => p.Description)
				.IsRequired()
				.HasMaxLength(MaxLengthDescription);

			builder
				.Property(p => p.PricePerSeat)
				.HasColumnType("decimal(18, 2)")
				.IsRequired();

			builder
				.Property(p => p.FreeSeats)
				.IsRequired();

			builder
				.Property(p => p.Baggage)
				.IsRequired();

			builder
				.Property(p => p.Pets)
				.IsRequired();

			builder
				.HasOne(p => p.Creator)
				.WithMany()
				.IsRequired()
				.HasForeignKey(p => p.CreatorId)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.HasOne(p => p.Group)
				.WithOne(g => g.Post)
				.HasForeignKey<Post>(p => p.Id);
		}
	}
}
