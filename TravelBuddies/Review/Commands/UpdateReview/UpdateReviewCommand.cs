namespace TravelBuddies.Application.Review.Commands.UpdateReview
{
	using MediatR;
	using TravelBuddies.Domain.Entities;


	public class UpdateReviewCommand : ReviewDto, IRequest<Review>
	{
	}
}
