namespace TravelBuddies.Application.Vehicle.Queries.GetVehicleById
{
	using MediatR;
	using System.Threading;
	using System.Threading.Tasks;
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;

	public class GetVehicleByIdHandler : BaseHandler, IRequestHandler<GetVehicleByIdQuery, Vehicle>
	{
		public GetVehicleByIdHandler(IRepository repository) : base(repository)
		{
		}

		public async Task<Vehicle> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
		{
			Vehicle? vehicle = await _repository.GetByIdAsync<Vehicle>(request.VehicleId);

			if (vehicle == null)
			{
				throw new VehicleNotFoundException($"Vehicle with id {request.VehicleId} not found");
			}

			return await Task.FromResult(vehicle);
		}
	}
}
