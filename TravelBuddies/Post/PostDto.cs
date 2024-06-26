﻿namespace TravelBuddies.Application.Post
{
	public class PostDto : BaseDto<int>
	{
		public int FromDestinationCityId { get; set; }

		public int ToDestinationCityId { get; set; }

		public required string Description { get; set; }

		public decimal PricePerSeat { get; set; }

		public required string Currency {  get; set; }

		public int FreeSeats { get; set; }

		public bool Baggage { get; set; }

		public bool Pets { get; set; }

		public bool IsCompleted { get; set; }

		public required string DateAndTime { get; set; }

		public string? PaymentLink { get; set; }

		public int PaymentType { get; set; }

		public required string CreatorId { get; set; }

		public int GroupId { get; set; }
	}
}
