﻿namespace TravelBuddies.Presentation.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TravelBuddies.Application.Review.Queries.GetReviewsByReciverId;
    using TravelBuddies.Domain.Entities;
    using TravelBuddies.Domain.Enums;
    using TravelBuddies.Application.Review.Commands.CreateReview;
    using TravelBuddies.Domain.Common;
    using TravelBuddies.Presentation.DTOs.Review;
    using TravelBuddies.Application.Review.Commands.UpdateReview;
    using TravelBuddies.Application.Review.Commands.DeleteReview;
    using Microsoft.AspNetCore.Cors;
    using TravelBuddies.Presentation.Extensions;
	using TravelBuddies.Application.User.Queries.GetUserById;
	using TravelBuddies.Application.Review.Queries.GetReviewsCountByReciverId;
	using TravelBuddies.Presentation.DTOs.User;
	using TravelBuddies.Application.Review.Queries.GetReviewsAvgRatingByReciverId;

	[EnableCors(ApplicationCorses.AllowOrigin)]
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class ReviewController : BaseController
	{
		public ReviewController(IMediator mediator)
			: base(mediator)
		{
		}

		[HttpGet]
		[Route("[action]")]
		public async Task<IActionResult> GetAllReviewByReciverId([FromQuery]string reciverId, [FromQuery]int page, [FromQuery]int pageCount)
		{
			ApplicationUser user = await _mediator.Send(new GetUserByIdQuery(reciverId));

			GetAllReviewsByReciverIdDto result = GetAllReviewsByReciverIdDto.FromUser(user);

			List<Review> reviews = await _mediator.Send(new GetReviewsByReciverIdQuery(reciverId, page, pageCount));

			result.Reviews = reviews.Select(ReviewDto.FromReview).ToList();
			result.CountReviews = await _mediator.Send(new GetReviewsCountByReciverIdQuery(reciverId));
			result.Rating = await _mediator.Send(new GetReviewsAvgRatingByReciverIdQuery(reciverId));

			return Ok(result);
		}

		[HttpPost]
		[Authorize(Policy = ApplicationPolicies.ClientAndDriver)]
		[Route("[action]")]
		public async Task<IActionResult> Create([FromBody] CreateReviewDto createReviewDto)
		{
			CreateReviewCommand command = new CreateReviewCommand()
			{
				CreatorId = User.Id(),
				ReciverId = createReviewDto.ReciverId,
				Text = createReviewDto.Text,
				Rating = createReviewDto.Rating,
			};

			await _mediator.Send(command);

			LogLevel logLevel = LogLevel.Information;
			string message = "Review created succesfully";

			await _fileLogger.LogAsync(logLevel, message);
			await _databaseLogger.LogAsync(logLevel, message);

			return Created();
		}

		[HttpPost]
		[Authorize(Policy = ApplicationPolicies.ClientAndDriver)]
		[Route("[action]")]
		public async Task<IActionResult> Update([FromBody] UpdateReviewDto updateReviewDto)
		{
			UpdateReviewCommand command = new UpdateReviewCommand()
			{
				Id = updateReviewDto.Id,
				CreatorId = User.Id(),
				ReciverId = updateReviewDto.ReciverId,
				Text = updateReviewDto.Text,
				Rating = updateReviewDto.Rating,
			};

			await _mediator.Send(command);

			LogLevel logLevel = LogLevel.Information;
			string message = "Review updated succesfully";

			await _fileLogger.LogAsync(logLevel, message);
			await _databaseLogger.LogAsync(logLevel, message);

			return Ok();
		}

		[HttpPost]
		[Route("[action]/{reviewId}")]
		public async Task<IActionResult> Delete(int reviewId)
		{
			await _mediator.Send(new DeleteReviewCommand(reviewId, User.Id()));

			LogLevel logLevel = LogLevel.Information;
			string message = "Review deleted succesfully";

			await _fileLogger.LogAsync(logLevel, message);
			await _databaseLogger.LogAsync(logLevel, message);

			return Ok();
		}
	}
}
