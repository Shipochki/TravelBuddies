using TravelBuddies.Domain.Common;

namespace TravelBuddies.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using TravelBuddies.Domain.EntityModels;
    using static DataConstants.ReviewConstants;

    public class Review : BaseSoftDeleteEntity<int>
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
	}
}
