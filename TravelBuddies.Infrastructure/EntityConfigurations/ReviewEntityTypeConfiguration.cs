namespace TravelBuddies.Infrastructure.EntityConfigurations
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using TravelBuddies.Domain.Entities;
	using static TravelBuddies.Domain.Common.DataConstants.ReviewConstants;

	public class ReviewEntityTypeConfiguration : IEntityTypeConfiguration<Review>
	{
		public void Configure(EntityTypeBuilder<Review> builder)
		{
			builder
				.HasOne(r => r.Creator)
				.WithMany()
				.HasForeignKey(r => r.CreatorId)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.Property(r => r.Rating)
				.IsRequired();

			builder
				.Property(r => r.Text)
				.HasMaxLength(MaxLengthText);

			builder
				.HasOne(r => r.Reciver)
				.WithMany()
				.HasForeignKey(r => r.ReciverId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
