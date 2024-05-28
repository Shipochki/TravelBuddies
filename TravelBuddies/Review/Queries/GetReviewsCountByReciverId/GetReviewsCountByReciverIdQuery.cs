namespace TravelBuddies.Application.Review.Queries.GetReviewsCountByReciverId
{
    using MediatR;

	public class GetReviewsCountByReciverIdQuery : IRequest<int>
	{
        public GetReviewsCountByReciverIdQuery(string reciverId)
        {
            ReciverId = reciverId;
        }
        public string ReciverId { get; set; }
	}
}
