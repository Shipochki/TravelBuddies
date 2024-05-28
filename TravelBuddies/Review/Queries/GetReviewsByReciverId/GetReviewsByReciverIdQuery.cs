namespace TravelBuddies.Application.Review.Queries.GetReviewsByReciverId
{
	using MediatR;
	using TravelBuddies.Domain.Entities;

	public record GetReviewsByReciverIdQuery : IRequest<List<Review>>
	{
        public GetReviewsByReciverIdQuery(string id, int page, int pageCount)
        {
            Id = id;
			Page = page;
			PageCount = pageCount;
        }
        public string Id { get; set; }

		public int Page { get; set; }

		public int PageCount { get; set; }
	}
}
