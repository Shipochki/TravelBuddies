﻿namespace TravelBuddies.Application.Post.Commands.UpdatePost
{
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using System.Threading;
    using TravelBuddies.Application.Exceptions;
    using TravelBuddies.Application.Repository;
    using TravelBuddies.Domain.Entities;
    using static TravelBuddies.Application.Exceptions.Messages.ExceptionMessages;

    public class UpdatePostHandler : BaseHandler, IRequestHandler<UpdatePostCommand, Task>
	{
		public UpdatePostHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager)
			: base(repository, userManager, roleManager)
		{
		}

		public async Task<Task> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
		{
			Post? post = await _repository.GetByIdAsync<Post>(request.Id);

			if (post == null)
			{
				throw new PostNotFoundException(
					string.Format(PostNotFoundMessage, request.Id));
			}

			City? fromDestination = await _repository.GetByIdAsync<City>(request.FromDestinationCityId);

			if (fromDestination == null)
			{
				throw new CityNotFoundException(
					string.Format(CityNotFoundMessage, request.FromDestinationCityId));
			}

			City? toDestination = await _repository.GetByIdAsync<City>(request.ToDestinationCityId);

			if (toDestination == null)
			{
				throw new CityNotFoundException(
					string.Format(CityNotFoundMessage, request.ToDestinationCityId));
			}

			post.FromDestinationCity = fromDestination;
			post.FromDestinationCityId = request.FromDestinationCityId;
			post.ToDestinationCity = toDestination;
			post.ToDestinationCityId = request.ToDestinationCityId;
			post.Description = request.Description;
			post.PricePerSeat = request.PricePerSeat;
			post.Baggage = request.Baggage;
			post.Pets = request.Pets;
			post.DateAndTime = DateTime.Parse(request.DateAndTime);

			_repository.Update(post);
			await _repository.SaveChangesAsync();
			return Task.CompletedTask;
		}
	}
}
