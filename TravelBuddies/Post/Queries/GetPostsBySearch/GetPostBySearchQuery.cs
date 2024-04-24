namespace TravelBuddies.Application.Post.Queries.GetPostsBySearch
{
	using MediatR;
	using TravelBuddies.Domain.Entities;

	public record GetPostBySearchQuery : IRequest<List<Post>>
	{
		public string? FromDate { get; set; }

		public string? ToDate { get; set; }

		public int FromDestinationCityId { get; set; }

		public int ToDestinationCityId { get; set; }

		public decimal? PriceMin { get; set; }

		public decimal? PriceMax { get; set; }

		public bool? Baggage { get; set; }

		public bool? Pets { get; set; }
	}
}
