namespace TravelBuddies.Domain.Entities
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using TravelBuddies.Domain.Models;
	using static DataConstants.ReviewConstants;

	public class Review : BaseEntity<int>, ISoftDeleteEntity
	{
		[Required]
		[ForeignKey(nameof(Creator))]
		public required string CreatorId { get; set; }
		public required ApplicationUser Creator {  get; set; }

		[Required]
		public int Rating { get; set; }

		[MaxLength(MaxLengthText)]
		public string? Text { get; set; }

		[Required]
		[ForeignKey(nameof(Reciver))]
		public required string ReciverId { get; set; }
		public required ApplicationUser Reciver { get; set; }

		public bool IsDeleted { get; set; }

		public DateTime DeletedOn { get; set; }
	}
}
