namespace TravelBuddies.Infrastructure.EntityConfigurations
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using TravelBuddies.Domain.Entities;
	using static TravelBuddies.Domain.Common.DataConstants.CityConstants;

	public class CityEntityTypeConfiguration : IEntityTypeConfiguration<City>
	{
		public void Configure(EntityTypeBuilder<City> builder)
		{
			builder
				.Property(c => c.Id)
				.IsRequired()
				.UseIdentityColumn();

			builder
				.Property(c => c.Name)
				.IsRequired()
				.HasMaxLength(MaxLengthCityName);

			builder
				.HasOne(c => c.Country)
				.WithMany()
				.HasForeignKey(c => c.Country);
		}
	}
}
