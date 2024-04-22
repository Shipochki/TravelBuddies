namespace TravelBuddies.Application.Vehicle.Commands.DeleteVehicle
{
    using MediatR;

	public class DeleteVehicleCommand : IRequest<Task>
	{
        public DeleteVehicleCommand(int vehicleId, string ownerId)
        {
            VehicleId = vehicleId;
            OwnerId = ownerId;
        }

        public int VehicleId { get; set; }

        public string OwnerId { get; set; }
    }
}
