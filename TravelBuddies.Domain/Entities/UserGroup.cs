namespace TravelBuddies.Domain.Entities
{
	using System.ComponentModel.DataAnnotations.Schema;
	using System.ComponentModel.DataAnnotations;

	public class UserGroup
	{
		[Required]
		[ForeignKey(nameof(User))]
		public required string UserId { get; set; }
		public required ApplicationUser User { get; set; }

		[Required]
		[ForeignKey(nameof(Group))]
		public int GroupId { get; set; }
		public required Group Group { get; set; }
	}
}
