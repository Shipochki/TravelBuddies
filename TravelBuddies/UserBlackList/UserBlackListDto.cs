namespace TravelBuddies.Application.UserBlackList
{
	public class UserBlackListDto
	{
		public required string OwnerId { get; set; }
		public required string UserId { get; set; }

		public int GroupId { get; set; }
	}
}
