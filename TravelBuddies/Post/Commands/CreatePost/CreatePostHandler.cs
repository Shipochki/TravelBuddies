namespace TravelBuddies.Application.Post.Commands.CreatePost
{
	using MediatR;
	using System.Threading;
	using System.Threading.Tasks;
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;
	using TravelBuddies.Domain.Enums;

	public class CreatePostHandler : BaseHandler, IRequestHandler<CreatePostCommand, Post>
	{
		public CreatePostHandler(IRepository repository) 
			: base(repository)
		{
		}

		public async Task<Post> Handle(CreatePostCommand request, CancellationToken cancellationToken)
		{
			ApplicationUser? creator = await _repository.GetByIdAsync<ApplicationUser>(request.CreatorId);

			if (creator == null)
			{
				throw new ApplicationUserNotFoundException($"Non-extitent User with Id {request.CreatorId}");
			}

			City? fromDestination = await _repository.GetByIdAsync<City>(request.FromDestinationCityId);

			if (fromDestination == null)
			{
				throw new CityNotFoundException($"Non-extitent City with Id {request.FromDestinationCityId}");
			}

			City? toDestination = await _repository.GetByIdAsync<City>(request.ToDestinationCityId);

			if (toDestination == null)
			{
				throw new CityNotFoundException($"Non-extitent City with Id {request.ToDestinationCityId}");
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

			if(post.PaymentType == PaymentType.Card || post.PaymentType == PaymentType.CashOrCard)
			{
				//Create StripeLink
				post.PaymentLink = string.Empty;
			}

			await _repository.AddAsync(post);
			await _repository.SaveChangesAsync();

			return await Task.FromResult(post);
		}
	}
}
