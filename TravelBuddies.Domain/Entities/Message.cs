namespace TravelBuddies.Domain.Entities
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using TravelBuddies.Domain.Models;
	using static DataConstants.MessageConstants;

	public class Message : BaseEntity<int>, ISoftDeleteEntity
	{
		[Required]
		[MaxLength(MaxLengthText)]
		public required string Text { get; set; }

		[Required]
		[ForeignKey(nameof(Creator))]
		public required string CreatorId { get; set; }
		public required ApplicationUser Creator {  get; set; }

		[Required]
		[ForeignKey(nameof(Group))]
		public int GroupId { get; set; }
		public required Group Group { get; set; }

		public bool IsDeleted { get; set; }

		public DateTime DeletedOn { get; set; }

		public DateTime SendTime = DateTime.Now;
	}
}
