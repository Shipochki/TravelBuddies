namespace TravelBuddies.Application.Review.Queries.GetReviewsAvgRatingByReciverId
{
using MediatR;

	public class GetReviewsAvgRatingByReciverIdQuery : IRequest<double>
	{
        public GetReviewsAvgRatingByReciverIdQuery(string reciverId)
        {
            ReciverId = reciverId;
        }

        public string ReciverId { get; set; }
	}
}
