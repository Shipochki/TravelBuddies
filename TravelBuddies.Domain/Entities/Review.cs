namespace TravelBuddies.Domain.Entities
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using static DataConstants.ReviewConstants;

	public class Review
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[ForeignKey(nameof(Creator))]
		public int CreatorId { get; set; }
		public required User Creator {  get; set; }

		[Required]
		public int Rating { get; set; }

		[MaxLength(MaxLengthText)]
		public string? Text { get; set; }

		[Required]
		[ForeignKey(nameof(Reciver))]
		public int ReciverId { get; set; }
		public required User Reciver { get; set; }

		public bool IsDeleted { get; set; }
	}
}
