namespace TravelBuddies.Application.Vehicle.Commands.DeleteVehicle
{
	using MediatR;
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;

	public class DeleteVehicleHandler : BaseHandler, IRequestHandler<DeleteVehicleCommand, Task>
	{
		public DeleteVehicleHandler(IRepository repository) : base(repository)
		{
		}

		public async Task<Task> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
		{
			Vehicle? vehicle = await _repository.GetByIdAsync<Vehicle>(request.VehicleId);

			if (vehicle == null)
			{
				throw new VehicleNotFoundException($"Vehicle with id {request.VehicleId} not found");
			}

			if(vehicle.OwnerId != request.OwnerId)
			{
				throw new ApplicationUserNotCreatorException($"User with id {request.OwnerId} is not Owner of vehicle with id {request.VehicleId}");
			}

			_repository.Delete(vehicle);
			await _repository.SaveChangesAsync();

			return Task.CompletedTask;
		}
	}
}
