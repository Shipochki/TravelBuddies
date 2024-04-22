namespace TravelBuddies.Presentation.Controllers
{
	using MediatR;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using TravelBuddies.Application.Review.Queries.GetReviewsByReciverId;
	using TravelBuddies.Domain.Entities;

	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class ReviewController : BaseController
	{
		public ReviewController(IMediator mediator) : base(mediator)
		{
		}

		[HttpPost]
		[Route("[action]")]
		public async Task<IActionResult> GetAllReviewByReciverId(string reciverId)
		{
			List<Review> reviews = await _mediator.Send(new GetReviewsByReciverIdQuery(reciverId));
			return Ok(reviews);
		}
	}
}
