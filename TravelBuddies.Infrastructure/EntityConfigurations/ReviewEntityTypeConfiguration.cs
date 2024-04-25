namespace TravelBuddies.Infrastructure.EntityConfigurations
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using TravelBuddies.Domain.Entities;


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
				.HasOne(r => r.Reciver)
				.WithMany()
				.HasForeignKey(r => r.ReciverId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
