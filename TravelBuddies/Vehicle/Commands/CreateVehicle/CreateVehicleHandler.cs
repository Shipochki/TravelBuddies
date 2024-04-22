namespace TravelBuddies.Application.Vehicle.Commands.CreateVehicle
{
	using MediatR;
	using System.Threading;
	using System.Threading.Tasks;
	using TravelBuddies.Domain.Entities;

	public class CreateVehicleHandler : IRequestHandler<CreateVehicleCommand, Vehicle>
	{
		public Task<Vehicle> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
