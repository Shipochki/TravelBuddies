namespace TravelBuddies.Domain.Entities
{
	public class UserGroup
	{
		public required string UserId { get; set; }
		public required ApplicationUser User { get; set; }

		public int GroupId { get; set; }
		public required Group Group { get; set; }
	}
}
