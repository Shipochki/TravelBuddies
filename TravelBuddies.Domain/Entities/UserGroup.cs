namespace TravelBuddies.Domain.Entities
{
	using System.ComponentModel.DataAnnotations.Schema;
	using System.ComponentModel.DataAnnotations;

	public class UserGroup
	{
		[Required]
		[ForeignKey(nameof(User))]
		public int UserId { get; set; }
		public User User { get; set; } = null!;

		[Required]
		[ForeignKey(nameof(Group))]
		public int GroupId { get; set; }
		public Group Group { get; set; } = null!;
	}
}
