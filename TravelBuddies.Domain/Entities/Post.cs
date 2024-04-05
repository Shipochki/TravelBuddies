namespace TravelBuddies.Domain.Entities
{
	using System.ComponentModel.DataAnnotations;
	using static DataConstants.PostConstants;

	public class Post
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(MaxLengthDestination)]
		public required string FromDestination { get; set; }

		[Required]
		[MaxLength(MaxLengthDestination)]
		public required string ToDestination { get; set;}

		[Required]
		[MaxLength(MaxLengthDescription)]
		public required string Description { get; set; }

		public decimal PriceForSeat { get; set; }

		public int FreeSeats { get; set; }

		public bool Baggage { get; set; }

		public bool Pets { get; set; }

		public bool IsCompleted { get; set; }

		public DateTime DateAndTime { get; set; }

		//CreatorId 

		//Creator user

		//GroupId null

		//Group 

		public bool IsDeleted { get; set; } = false;
	}
}
