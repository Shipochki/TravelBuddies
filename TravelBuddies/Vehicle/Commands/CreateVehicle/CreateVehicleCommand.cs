namespace TravelBuddies.Application.Vehicle.Commands.CreateVehicle
{
	using MediatR;
	using TravelBuddies.Domain.Entities;

	public class CreateVehicleCommand : VehicleDto, IRequest<Vehicle>
	{
	}
}
