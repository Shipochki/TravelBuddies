﻿namespace TravelBuddies.Infrastructure.EntityConfigurations
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using TravelBuddies.Domain.Entities;

	public class GroupEntityTypeConfiguration : IEntityTypeConfiguration<Group>
	{
		public void Configure(EntityTypeBuilder<Group> builder)
		{
			builder
				.HasOne(g => g.Creator)
				.WithMany()
				.HasForeignKey(g => g.CreatorId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.HasOne(g => g.Post)
				.WithOne(g => g.Group)
				.HasForeignKey<Group>(g => g.PostId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
