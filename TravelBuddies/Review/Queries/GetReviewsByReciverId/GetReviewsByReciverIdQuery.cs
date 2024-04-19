namespace TravelBuddies.Application.Review.Queries.GetReviewsByReciverId
{
	using MediatR;
	using TravelBuddies.Domain.Entities;

	public record GetReviewsByReciverIdQuery : IRequest<IEnumerable<Review>>
	{
        public GetReviewsByReciverIdQuery(string id)
        {
            Id = id;
        }
        public string Id { get; set; }
	}
}
