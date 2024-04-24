namespace TravelBuddies.Presentation.DTOs.Post
{
	public class PostSearchDto
	{
		public string? FromDate { get; set; }

		public string? ToDate { get; set;}

		public int FromDestinationCityId { get; set; }

		public int ToDestinationCityId { get; set;}

		public decimal? PriceMin { get; set; }

		public decimal? PriceMax { get; set; }

		public bool? Baggage { get; set; }

		public bool? Pets { get; set; }
	}
}
