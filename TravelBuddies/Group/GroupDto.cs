namespace TravelBuddies.Application.Group
{
	public class GroupDto : BaseDto<int>
	{
		public int PostId { get; set; }

		public required string CreatorId { get; set; }
	}
}
