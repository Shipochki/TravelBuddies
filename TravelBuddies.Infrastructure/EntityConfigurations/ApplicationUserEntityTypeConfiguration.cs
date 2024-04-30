namespace TravelBuddies.Infrastructure.EntityConfigurations
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using TravelBuddies.Domain.Entities;
	using static TravelBuddies.Domain.Common.DataConstants.UserConstants;
	using static TravelBuddies.Domain.Common.DataConstants.CityConstants;
	using static TravelBuddies.Domain.Common.DataConstants.CountryConstants;

	public class ApplicationUserEntityTypeConfiguration : IEntityTypeConfiguration<ApplicationUser>
	{
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			builder
				.Property(a => a.Id)
				.IsRequired()
				.UseIdentityColumn();

			builder
				.Property(a => a.FirstName)
				.HasMaxLength(MaxLengthFirstName);

			builder
				.Property(a => a.LastName)
				.HasMaxLength(MaxLengthLastName);

			builder
				.Property(a => a.City)
				.HasMaxLength(MaxLengthCityName);

			builder
				.Property(a => a.Country)
				.HasMaxLength(MaxLengthCountryName);
		}
	}
}
