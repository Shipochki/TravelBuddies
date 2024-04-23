namespace TravelBuddies.Application.Vehicle.Commands.CreateVehicle
{
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using System.Threading;
    using System.Threading.Tasks;
    using TravelBuddies.Application.Exceptions;
    using TravelBuddies.Application.Repository;
    using TravelBuddies.Domain.Entities;
    using TravelBuddies.Domain.Enums;
    using static TravelBuddies.Application.Exceptions.Messages.ExceptionMessages;

    public class CreateVehicleHandler : BaseHandler, IRequestHandler<CreateVehicleCommand, Vehicle>
	{
		public CreateVehicleHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager)
			: base(repository, userManager, roleManager)
		{
		}

		public async Task<Vehicle> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
		{
			ApplicationUser? owner = await _userManager.FindByIdAsync(request.OwnerId);

			if (owner == null)
			{
				throw new ApplicationUserNotFoundException(
					string.Format(ApplicationUserNotFoundMessage, request.OwnerId));
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
