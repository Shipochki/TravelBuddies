namespace TravelBuddies.Application.Review.Queries.GetReviewsByReciverId
{
	using MediatR;
	using TravelBuddies.Domain.Entities;

	public record GetReviewsByReciverIdCommand : IRequest<IEnumerable<Review>>
	{
        public GetReviewsByReciverIdCommand(string id)
        {
            Id = id;
        }
        public string Id { get; set; }
	}
}
