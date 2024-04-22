namespace TravelBuddies.Application.Vehicle.Commands.CreateVehicle
{
	using MediatR;
	using System.Threading;
	using System.Threading.Tasks;
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;
	using TravelBuddies.Domain.Enums;

	public class CreateVehicleHandler : BaseHandler, IRequestHandler<CreateVehicleCommand, Vehicle>
	{
		public CreateVehicleHandler(IRepository repository) : base(repository)
		{
		}

		public async Task<Vehicle> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
		{
			ApplicationUser? owner = await _repository.GetByIdAsync<ApplicationUser>(request.OwnerId);

			if (owner == null)
			{
				throw new ApplicationUserNotFoundException($"User with id {request.OwnerId} not found.");
			}

			Vehicle vehicle = new Vehicle()
			{
				BrandName = request.BrandName,
				ModelName = request.ModelName,
				Fuel = (Fuel)request.Fuel,
				SeatCount = request.SeatCount,
				PictureLink = request.PictureLink,
				ACSystem = request.ACSystem,
				Owner = owner,
				OwnerId = request.OwnerId,
				CreatedOn = DateTime.Now
			};

			await _repository.AddAsync(vehicle);
			await _repository.SaveChangesAsync();

			return await Task.FromResult(vehicle);
		}
	}
}
