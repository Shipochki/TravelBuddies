namespace TravelBuddies.Infrastructure.EntityConfigurations
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using TravelBuddies.Domain.Entities;
	using static TravelBuddies.Domain.Common.DataConstants.CountryConstants;

	public class CountryEntityTypeConfiguration : IEntityTypeConfiguration<Country>
	{
		public void Configure(EntityTypeBuilder<Country> builder)
		{
			builder
				.Property(c => c.Name)
				.IsRequired()
				.HasMaxLength(MaxLengthCountryName);
		}
	}
}
