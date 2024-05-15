namespace TravelBuddies.Application.Post.Commands.CreatePost
{
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using System.Threading;
    using System.Threading.Tasks;
    using TravelBuddies.Application.Common.Interfaces.Stripe;
    using TravelBuddies.Application.Common.Exceptions;
    using TravelBuddies.Domain.Entities;
    using TravelBuddies.Domain.Enums;
    using static TravelBuddies.Application.Common.Exceptions.Messages.ExceptionMessages;
    using TravelBuddies.Application.Common.Interfaces.Repository;

    public class CreatePostHandler : BaseHandler, IRequestHandler<CreatePostCommand, Post>
	{
		private readonly IStripeService _stripeService;

		public CreatePostHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager
			, IStripeService stripeService)
			: base(repository, userManager, roleManager)
		{
			_stripeService = stripeService;
		}

		public async Task<Post> Handle(CreatePostCommand request, CancellationToken cancellationToken)
		{
			ApplicationUser? creator = await _userManager.FindByIdAsync(request.CreatorId);

			if (creator == null)
			{
				throw new ApplicationUserNotFoundException(
					string.Format(ApplicationUserNotFoundMessage, request.CreatorId));
			}

			City? fromDestination = await _repository.GetByIdAsync<City>(request.FromDestinationCityId);

			if (fromDestination == null)
			{
				throw new CityNotFoundException(
					string.Format(CityNotFoundMessage, request.FromDestinationCityId));
			}

			City? toDestination = await _repository.GetByIdAsync<City>(request.ToDestinationCityId);

			if (toDestination == null)
			{
				throw new CityNotFoundException(
					string.Format(CityNotFoundMessage, request.ToDestinationCityId));
			}

			Post post = new Post()
			{
				FromDestinationCity = fromDestination,
				FromDestinationCityId = fromDestination.Id,
				ToDestinationCity = toDestination,
				ToDestinationCityId = toDestination.Id,
				Description = request.Description,
				PricePerSeat = request.PricePerSeat,
				FreeSeats = request.FreeSeats,
				Baggage = request.Baggage,
				Pets = request.Pets,
				DateAndTime = DateTime.Parse(request.DateAndTime),
				PaymentType = (PaymentType)request.PaymentType,
				Creator = creator,
				CreatorId = creator.Id,
				CreatedOn = DateTime.Now,
			};

			if(post.PaymentType == PaymentType.Card || post.PaymentType == PaymentType.CashAndCard)
			{
				post.PaymentLink = _stripeService.CreateProduct(post);
			}

			await _repository.AddAsync(post);
			await _repository.SaveChangesAsync();

			return await Task.FromResult(post);
		}
	}
}
