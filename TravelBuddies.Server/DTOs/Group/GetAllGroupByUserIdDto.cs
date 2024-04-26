namespace TravelBuddies.Presentation.DTOs.Group
{
	using TravelBuddies.Domain.Entities;

	public class GetAllGroupByUserIdDto
	{
		public int Id { get; set; }

		public required string CreatorId { get; set; } 

		public int PostId { get; set; }

		public static GetAllGroupByUserIdDto FromGroup(Group group)
		{
			return new GetAllGroupByUserIdDto()
			{
				Id = group.Id,
				CreatorId = group.CreatorId,
				PostId = group.PostId,
			};
		}
	}
}
