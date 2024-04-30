namespace TravelBuddies.Infrastructure.EntityConfigurations
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using TravelBuddies.Domain.Entities;
	using static TravelBuddies.Domain.Common.DataConstants.VehicleConstants;

	public class VehicleEntityTypeConfiguration : IEntityTypeConfiguration<Vehicle>
	{
		public void Configure(EntityTypeBuilder<Vehicle> builder)
		{
			builder
				.Property(v => v.Id)
				.IsRequired()
				.UseIdentityColumn();

			builder
				.Property(v => v.BrandName)
				.IsRequired()
				.HasMaxLength(MaxLengthBrandName);

			builder
				.Property(v => v.ModelName)
				.IsRequired()
				.HasMaxLength(MaxLengthModelName);

			builder
				.HasOne(v => v.Owner)
				.WithOne()
				.HasForeignKey<ApplicationUser>(a => a.Id)
				.IsRequired();
		}
	}
}
