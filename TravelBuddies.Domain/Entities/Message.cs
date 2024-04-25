using TravelBuddies.Domain.Common;

namespace TravelBuddies.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using TravelBuddies.Domain.EntityModels;
    using static DataConstants.MessageConstants;

    public class Message : BaseSoftDeleteEntity<int>
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
	}
}
