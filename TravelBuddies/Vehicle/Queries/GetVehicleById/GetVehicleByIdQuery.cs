namespace TravelBuddies.Application.Vehicle.Queries.GetVehicleById
{
	using MediatR;
	using TravelBuddies.Domain.Entities;

	public record GetVehicleByIdQuery : IRequest<Vehicle>
	{
        public GetVehicleByIdQuery(int vehicleId)
        {
            VehicleId = vehicleId;
        }

        public int VehicleId { get; set; }
    }
}
