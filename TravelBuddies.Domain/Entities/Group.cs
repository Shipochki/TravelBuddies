namespace TravelBuddies.Domain.Entities
{
	using System.ComponentModel.DataAnnotations;

	public class Group
	{
		[Key]
		public int Id { get; set; }

		//PostId

		//Post

		//CreatorId

		//Creator

		public bool IsDeleted { get; set; } = false;

		//List UserGroup

		//List Message
	}
}
