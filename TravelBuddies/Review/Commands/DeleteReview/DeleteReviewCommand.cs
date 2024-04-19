namespace TravelBuddies.Application.Review.Commands.DeleteReview
{
	using MediatR;

	public record DeleteReviewCommand : IRequest<Task>
	{
        public DeleteReviewCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
	}
}
