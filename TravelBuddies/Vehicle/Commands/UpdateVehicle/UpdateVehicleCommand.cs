namespace TravelBuddies.Application.Vehicle.Commands.UpdateVehicle
{
	using MediatR;

	public class UpdateVehicleCommand : VehicleDto, IRequest<Task>
	{
	}
}
