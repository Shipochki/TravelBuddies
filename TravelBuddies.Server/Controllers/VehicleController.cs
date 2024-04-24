﻿namespace TravelBuddies.Presentation.Controllers
{
	using MediatR;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using TravelBuddies.Application.Constants;
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Vehicle.Commands.CreateVehicle;
	using TravelBuddies.Application.Vehicle.Commands.DeleteVehicle;
	using TravelBuddies.Application.Vehicle.Queries.GetVehicleById;
	using TravelBuddies.Domain.Entities;
	using TravelBuddies.Domain.Enums;
	using TravelBuddies.Presentation.Configurations;
	using TravelBuddies.Presentation.DTOs.Vehicle;

	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class VehicleController : BaseController
	{
		public VehicleController(IMediator mediator)
			: base(mediator)
		{
		}

		[HttpPost]
		[Route("[action]")]
		[Authorize(Policy = ApplicationPolicies.OnlyDriver)]
		public async Task<IActionResult> Create([FromBody]CreateVehicleDto createVehicleDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			LogLevel logLevel;
			string message;

			try
			{
				CreateVehicleCommand command = new CreateVehicleCommand()
				{
					BrandName = createVehicleDto.BrandName,
					ModelName = createVehicleDto.ModelName,
					Fuel = createVehicleDto.Fuel,
					SeatCount = createVehicleDto.SeatCount,
					PictureLink = createVehicleDto.PictureLink,
					ACSystem = createVehicleDto.ACSystem,
					OwnerId = createVehicleDto.OwnerId,
				};

				await _mediator.Send(command);

				logLevel = LogLevel.Information;
				message = "Succesfully created vehicle";

				await _fileLogger.LogAsync(logLevel, message);
				await _databaseLogger.LogAsync(logLevel, message);

				return Created();
			}
			catch (ApplicationUserNotFoundException m)
			{
				logLevel = LogLevel.Error;

				await _fileLogger.LogAsync(logLevel, m.Message);
				await _databaseLogger.LogAsync(logLevel, m.Message);

				return NotFound(m.Message);
			}
		}

		[HttpPost]
		[Route("[action]")]
		[Authorize(Policy = ApplicationPolicies.DriverAndAdmin)]
		public async Task<IActionResult> Delete(int vehicleId)
		{
			LogLevel logLevel;
			string message;

			try
			{
				await _mediator.Send(new DeleteVehicleCommand(vehicleId, User.Id()));

				logLevel = LogLevel.Information;
				message = "Succesfully deleted vehhicle";

				await _fileLogger.LogAsync(logLevel, message);
				await _databaseLogger.LogAsync(logLevel, message);

				return Ok(message);
			}
			catch (VehicleNotFoundException m)
			{
				logLevel = LogLevel.Error;

				await _fileLogger.LogAsync(logLevel, m.Message);
				await _databaseLogger.LogAsync(logLevel, m.Message);

				return NotFound(m.Message);
			}
			catch (ApplicationUserNotCreatorException m)
			{
				logLevel = LogLevel.Error;

				await _fileLogger.LogAsync(logLevel, m.Message);
				await _databaseLogger.LogAsync(logLevel, m.Message);

				return Unauthorized(m.Message);
			}
			catch (ApplicationUserNotFoundException m)
			{
				logLevel = LogLevel.Error;

				await _fileLogger.LogAsync(logLevel, m.Message);
				await _databaseLogger.LogAsync(logLevel, m.Message);

				return NotFound(m.Message);
			}
			
		}

		[HttpGet]
		[Route("[action]")]
		public async Task<IActionResult> GetVehicleById(int vehicleId)
		{
			LogLevel logLevel;
			string message;

			try
			{
				Vehicle vehicle = await _mediator.Send(new GetVehicleByIdQuery(vehicleId));

				logLevel = LogLevel.Information;
				message = "Succesfully get vehicle";

				await _fileLogger.LogAsync(logLevel, message);
				await _databaseLogger.LogAsync(logLevel, message);

				return Ok(GetVehicleByIdDto.FromVehicle(vehicle));
			}
			catch (VehicleNotFoundException m)
			{
				logLevel = LogLevel.Error;

				await _fileLogger.LogAsync(logLevel, m.Message);
				await _databaseLogger.LogAsync(logLevel, m.Message);

				return NotFound(m.Message);
			}
		}
	}
}
