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
		public required string Currency {  get; set; }

		public int FreeSeats { get; set; }

		public bool Baggage { get; set; }

		public bool Pets { get; set; }

		public required string DateAndTime { get; set; }

		public required string CreatorId { get; set; }

		public int PaymentType {  get; set; }

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
				Currency = post.Currency,
				FreeSeats = post.FreeSeats,
				Baggage = post.Baggage,
				Pets = post.Pets,
				CreatorId = post.CreatorId,
				DateAndTime = post.DateAndTime.ToString("MM.dd.yyyy hh:mm tt"),
				GroupId = post.GroupId,
				PaymentType = (int)post.PaymentType
			};
		}
	}
}
