﻿namespace TravelBuddies.Domain.Entities
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Group
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[ForeignKey(nameof(Post))]
		public int PostId { get; set; }
		public required Post Post { get; set; }

		[Required]
		[ForeignKey(nameof(Creator))]
		public required string CreatorId { get; set; }
		public required User Creator { get; set; }

		public bool IsDeleted { get; set; } = false;

		public List<UserGroup> UsersGroups { get; set; } = new List<UserGroup>();

		public List<Message> Messages { get; set; } = new List<Message>();
	}
}
