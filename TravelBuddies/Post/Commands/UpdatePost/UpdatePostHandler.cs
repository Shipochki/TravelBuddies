namespace TravelBuddies.Application.Post.Commands.UpdatePost
{
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using System.Threading;
    using TravelBuddies.Domain.Entities;
    using static TravelBuddies.Application.Common.Exceptions.Messages.ExceptionMessages;
    using TravelBuddies.Application.Common.Interfaces.Repository;
	using TravelBuddies.Application.Common.Exceptions.NotFound;
	using TravelBuddies.Application.Common.Exceptions.Forbidden;
	using TravelBuddies.Domain.Enums;
	using TravelBuddies.Application.Common.Interfaces.Stripe;
	using Microsoft.EntityFrameworkCore;

	public class UpdatePostHandler : BaseHandler, IRequestHandler<UpdatePostCommand, Task>
	{
		private readonly IStripeService _stripeService;

		public UpdatePostHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager
			, IStripeService stripeService)
			: base(repository, userManager, roleManager)
		{
			_stripeService = stripeService;
		}

		public async Task<Task> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
		{
			Post? post = await _repository
				.All<Post>(r => r.Id == request.Id)
				.Include(r => r.Creator)
				.FirstOrDefaultAsync();

			if (post == null)
			{
				throw new PostNotFoundException(
					string.Format(PostNotFoundMessage, request.Id));
			}

			City? fromDestination = await _repository
				.GetByIdAsync<City>(request.FromDestinationCityId);

			if (fromDestination == null)
			{
				throw new CityNotFoundException(
					string.Format(CityNotFoundMessage, request.FromDestinationCityId));
			}

			City? toDestination = await _repository
				.GetByIdAsync<City>(request.ToDestinationCityId);

			if (toDestination == null)
			{
				throw new CityNotFoundException(
					string.Format(CityNotFoundMessage, request.ToDestinationCityId));
			}

			if(post.CreatorId != request.CreatorId)
			{
				throw new ApplicationUserNotCreatorException(
					string.Format(ApplicationUserNotCreatorMessage, request.CreatorId));
			}

			if(string.IsNullOrEmpty(post.PaymentLink) 
				&& (PaymentType)request.PaymentType == PaymentType.Card 
				|| (PaymentType)request.PaymentType == PaymentType.CashAndCard)
			{
				post.PaymentLink = _stripeService.CreateProduct(post);
			}

			post.FromDestinationCity = fromDestination;
			post.FromDestinationCityId = request.FromDestinationCityId;
			post.ToDestinationCity = toDestination;
			post.ToDestinationCityId = request.ToDestinationCityId;
			post.Description = request.Description;
			post.PricePerSeat = request.PricePerSeat;
			post.PaymentType = (PaymentType)request.PaymentType;
			post.Currency = request.Currency;
			post.Baggage = request.Baggage;
			post.Pets = request.Pets;
			post.FreeSeats = request.FreeSeats;
			post.DateAndTime = DateTime.Parse(request.DateAndTime);
			post.UpdatedOn = DateTime.Now;

			await _repository.SaveChangesAsync();
			return Task.CompletedTask;
		}
	}
}
