namespace TravelBuddies.Application.Review.Commands.DeleteReview
{
	using MediatR;

	public record DeleteReviewCommand : IRequest<Task>
	{
        public DeleteReviewCommand(int reviewId, string creatorId)
        {
            ReviewId = reviewId;
            CreatorId = creatorId;
        }

        public int ReviewId { get; set; }

        public string CreatorId { get; set; }
	}
}
