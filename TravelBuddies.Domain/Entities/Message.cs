namespace TravelBuddies.Domain.Entities
{
	using System.ComponentModel.DataAnnotations;
	using static DataConstants.MessageConstants;

	public class Message
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(MaxLengthText)]
		public required string Text { get; set; }

		//CreatorId

		//Creator user

		//GroupId

		//Group

		[Required]
		public DateTime SendTime = DateTime.Now;

		public bool IsDeleted { get; set; } = false;
	}
}
