namespace TravelBuddies.Application.Vehicle.Commands.CreateVehicle
{
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using System.Threading;
    using System.Threading.Tasks;
    using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Interfaces.AzureStorage;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;
    using TravelBuddies.Domain.Enums;
    using static TravelBuddies.Application.Exceptions.Messages.ExceptionMessages;

    public class CreateVehicleHandler : BaseHandler, IRequestHandler<CreateVehicleCommand, Vehicle>
	{
		private readonly IBlobService _blobService;
		public CreateVehicleHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager
			, IBlobService blobService)
			: base(repository, userManager, roleManager)
		{
			_blobService = blobService;
		}

		public async Task<Vehicle> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
		{
			ApplicationUser? owner = await _userManager.FindByIdAsync(request.OwnerId);

			if (owner == null)
			{
				throw new ApplicationUserNotFoundException(
					string.Format(ApplicationUserNotFoundMessage, request.OwnerId));
			}

			string pictureLink = await _blobService.UploadImageAsync(request.PictureLink);

			Vehicle vehicle = new Vehicle()
			{
				BrandName = request.BrandName,
				ModelName = request.ModelName,
				Fuel = (Fuel)request.Fuel,
				SeatCount = request.SeatCount,
				PictureLink = pictureLink,
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
