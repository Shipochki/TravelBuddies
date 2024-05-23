namespace TravelBuddies.Presentation.DTOs.UserBlackList
{
	public class CreateUserBlackListDto
	{
		public int GroupId { get; set; }

		public required string UserId { get; set; }
	}
}
