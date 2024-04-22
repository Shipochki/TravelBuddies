namespace TravelBuddies.Presentation.Controllers
{
	using MediatR;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Review.Queries.GetReviewsByReciverId;
	using TravelBuddies.Domain.Entities;
	using TravelBuddies.Presentation.DTOs;
	using TravelBuddies.Domain.Enums;
	using TravelBuddies.Application.Review.Commands.CreateReview;
	using TravelBuddies.Presentation.Constants;

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

		[HttpPost]
		[Authorize(Policy = ApplicationPolicies.ClientAndDriver)]
		[Route("[action]")]
		public async Task<IActionResult> CreateReview([FromBody]CreateReviewDto createReviewDto)
		{
			LogLevel logLevel;
			string message;

			try
			{
				CreateReviewCommand command = new CreateReviewCommand()
				{
					CreatorId = createReviewDto.CreatorId,
					ReciverId = createReviewDto.ReciverId,
					Text = createReviewDto.Text,
					Rating = createReviewDto.Rating,
				};

				await _mediator.Send(command);

				logLevel = LogLevel.Information;
				message = "Review created succesfully";

				await _fileLogger.LogAsync(logLevel, message);
				await _databaseLogger.LogAsync(logLevel, message);

				return Ok(message);
			}
			catch (ApplicationUserNotFoundException m)
			{
				logLevel = LogLevel.Error;
				message = "Review not created succesfully";

				await _fileLogger.LogAsync(logLevel, message);
				await _databaseLogger.LogAsync(logLevel, message);
				return BadRequest(m.Message);
			}
		}
	}
}
