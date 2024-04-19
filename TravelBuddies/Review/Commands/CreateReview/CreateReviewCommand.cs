namespace TravelBuddies.Application.Review.Commands.CreateReview
{
	using MediatR;
	using TravelBuddies.Domain.Entities;

	public class CreateReviewCommand : ReviewDto, IRequest<Review>
	{
	}
}
