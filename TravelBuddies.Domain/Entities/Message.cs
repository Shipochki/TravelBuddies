namespace TravelBuddies.Domain.Entities
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using static DataConstants.MessageConstants;

	public class Message
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(MaxLengthText)]
		public required string Text { get; set; }

		[Required]
		[ForeignKey(nameof(Creator))]
		public int CreatorId { get; set; }
		public required User Creator {  get; set; }

		[Required]
		[ForeignKey(nameof(Group))]
		public int GroupId { get; set; }
		public required Group Group { get; set; }

		[Required]
		public DateTime SendTime = DateTime.Now;

		public bool IsDeleted { get; set; } = false;
	}
}
