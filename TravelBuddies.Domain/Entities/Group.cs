namespace TravelBuddies.Domain.Entities
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using TravelBuddies.Domain.EntityModels;

	public class Group : BaseEntity<int>, ISoftDeleteEntity
	{
		[Required]
		[ForeignKey(nameof(Post))]
		public int PostId { get; set; }
		public required Post Post { get; set; }

		[Required]
		[ForeignKey(nameof(Creator))]
		public required string CreatorId { get; set; }
		public required ApplicationUser Creator { get; set; }

		public List<UserGroup> UsersGroups { get; set; } = new List<UserGroup>();

		public List<Message> Messages { get; set; } = new List<Message>();

		public bool IsDeleted { get; set; }

		public DateTime DeletedOn { get; set; }
	}
}
