namespace TravelBuddies.Presentation.DTOs.Post
{
	using TravelBuddies.Domain.Entities;

	public class PostDto
	{
		public int Id { get; set; }

		public required string FromDestinationName { get; set; }

		public required string ToDestinationName { get; set; }

		public required string Description { get; set; }

		public decimal PricePerSeat { get; set; }

		public int FreeSeats { get; set; }

		public bool Baggage { get; set; }

		public bool Pets { get; set; }

		public required string DateAndTime { get; set; }

		public int? GroupId { get; set; }

		public static PostDto FromPost(Post post)
		{
			return new PostDto()
			{
				Id = post.Id,
				FromDestinationName = post.FromDestinationCity.Name,
				ToDestinationName = post.ToDestinationCity.Name,
				Description = post.Description,
				PricePerSeat = post.PricePerSeat,
				FreeSeats = post.FreeSeats,
				Baggage = post.Baggage,
				Pets = post.Pets,
				DateAndTime = post.DateAndTime.ToString(),
				GroupId = post.GroupId,
			};
		}
	}
}
