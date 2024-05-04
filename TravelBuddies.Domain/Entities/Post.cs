namespace TravelBuddies.Domain.Entities
{
    using TravelBuddies.Domain.Enums;
    using TravelBuddies.Domain.EntityModels;

    public class Post : BaseSoftDeleteEntity<int>
	{
		public int FromDestinationCityId { get; set; }
		public required City FromDestinationCity {  get; set; }

		public int ToDestinationCityId { get; set; }
		public required City ToDestinationCity { get; set; }

		public required string Description { get; set; }

		public decimal PricePerSeat { get; set; }

		public int FreeSeats { get; set; }

		public bool Baggage { get; set; }

		public bool Pets { get; set; }
			
		public bool IsCompleted { get; set; }

		public DateTime DateAndTime { get; set; }

		public string? PaymentLink { get; set; }

		public PaymentType PaymentType { get; set; }

		public required string CreatorId { get; set; }
		public required ApplicationUser Creator { get; set; }

		public int? GroupId { get; set; }
		public Group? Group { get; set; }
	}
}
