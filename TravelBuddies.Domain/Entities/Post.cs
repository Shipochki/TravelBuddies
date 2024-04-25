using TravelBuddies.Domain.Common;

namespace TravelBuddies.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using TravelBuddies.Domain.Enums;
    using TravelBuddies.Domain.EntityModels;
    using static DataConstants.PostConstants;

    public class Post : BaseSoftDeleteEntity<int>
	{
		[Required]
		[ForeignKey(nameof(FromDestinationCity))]
		public int FromDestinationCityId { get; set; }
		public required City FromDestinationCity {  get; set; }

		[Required]
		[ForeignKey(nameof(ToDestinationCity))]
		public int ToDestinationCityId { get; set; }
		public required City ToDestinationCity { get; set; }

		[Required]
		[MaxLength(MaxLengthDescription)]
		public required string Description { get; set; }

		[Required]
		public decimal PricePerSeat { get; set; }

		[Required]
		public int FreeSeats { get; set; }

		[Required]
		public bool Baggage { get; set; }

		[Required]
		public bool Pets { get; set; }
			
		public bool IsCompleted { get; set; }

		public DateTime DateAndTime { get; set; }

		public string? PaymentLink { get; set; }

		public PaymentType PaymentType { get; set; }

		[Required]
		[ForeignKey(nameof(Creator))]
		public required string CreatorId { get; set; }
		public required ApplicationUser Creator { get; set; }

		[ForeignKey(nameof(Group))]
		public int? GroupId { get; set; }
		public Group? Group { get; set; }
	}
}
