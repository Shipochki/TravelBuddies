namespace TravelBuddies.Presentation.DTOs.Post
{
    using System.ComponentModel.DataAnnotations;
    using static TravelBuddies.Domain.Constants.DataConstants.PostConstants;

    public class UpdatePostDto
	{
		public int Id { get; set; }

		public int FromDestinationCityId { get; set; }

		public int ToDestinationCityId { get; set; }

		[Required]
		[MinLength(MinLengthDescription)]
		[MaxLength(MaxLengthDescription)]
		public required string Description { get; set; }

		public decimal PricePerSeat { get; set; }

		public int FreeSeats { get; set; }

		public bool Baggage { get; set; }

		public bool Pets { get; set; }

		public required string DateAndTime { get; set; }

		public int PaymentType { get; set; }

		public int CreatorId { get; set; }
	}
}
