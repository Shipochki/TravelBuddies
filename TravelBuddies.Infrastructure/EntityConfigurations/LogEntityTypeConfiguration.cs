namespace TravelBuddies.Infrastructure.EntityConfigurations
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using TravelBuddies.Domain.Entities;
	using static TravelBuddies.Domain.Common.DataConstants.LogConstants;

	public class LogEntityTypeConfiguration : IEntityTypeConfiguration<Log>
	{
		public void Configure(EntityTypeBuilder<Log> builder)
		{
			builder
				.Property(l => l.Message)
				.IsRequired()
				.HasMaxLength(MaxLengthMessage);
		}
	}
}
