﻿namespace TravelBuddies.Infrastructure.EntityConfigurations
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using TravelBuddies.Domain.Entities;
	using static TravelBuddies.Domain.Common.DataConstants.MessageConstants;

	public class MessageEntityTypeConfiguration : IEntityTypeConfiguration<Message>
	{
		public void Configure(EntityTypeBuilder<Message> builder)
		{
			builder
				.Property(m => m.Text)
				.IsRequired()
				.HasMaxLength(MaxLengthText);

			builder
				.HasOne(m => m.Creator)
				.WithMany()
				.HasForeignKey(m => m.CreatorId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.HasOne(m => m.Group)
				.WithMany(m => m.Messages)
				.HasForeignKey(m => m.GroupId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
