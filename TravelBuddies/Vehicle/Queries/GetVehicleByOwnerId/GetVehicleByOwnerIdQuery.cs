namespace TravelBuddies.Application.Vehicle.Queries.GetVehicleByOwnerId
{
	using MediatR;
	using TravelBuddies.Domain.Entities;

	public class GetVehicleByOwnerIdQuery : IRequest<Vehicle?>
	{
        public GetVehicleByOwnerIdQuery(string ownerId)
        {
            OwnerId = ownerId;
        }

        public string OwnerId { get; set; }
    }
}
