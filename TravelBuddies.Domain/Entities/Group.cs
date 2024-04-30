namespace TravelBuddies.Domain.Entities
{
	using TravelBuddies.Domain.EntityModels;

	public class Group : BaseSoftDeleteEntity<int>
	{
		public int PostId { get; set; }
		public required Post Post { get; set; }

		public required string CreatorId { get; set; }
		public required ApplicationUser Creator { get; set; }

		public List<UserGroup> UsersGroups { get; set; } = new List<UserGroup>();

		public List<Message> Messages { get; set; } = new List<Message>();
	}
}
