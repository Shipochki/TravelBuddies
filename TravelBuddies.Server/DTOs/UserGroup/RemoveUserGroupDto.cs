namespace TravelBuddies.Presentation.DTOs.UserGroup
{
	public class RemoveUserGroupDto
	{
		public int GroupId { get; set; }

		public required string UserId { get; set; }
	}
}
