﻿namespace TravelBuddies.Application.Message.Commands.CreateMessage
{
	using MediatR;
	using Microsoft.AspNetCore.Identity;
	using System.Threading;
	using System.Threading.Tasks;
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;
	using static TravelBuddies.Application.Exceptions.ExceptionMessages;

	public class CreateMessageHandler : BaseHandler, IRequestHandler<CreateMessageCommand, Message>
	{
		public CreateMessageHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager)
			: base(repository, userManager, roleManager)
		{
		}

		public async Task<Message> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
		{
			ApplicationUser? creator = await _userManager.FindByIdAsync(request.CreatorId);

			if (creator == null)
			{
				throw new ApplicationUserNotFoundException(
					string.Format(ApplicationUserNotFoundMessage, request.CreatorId));
			}

			Group? group = await _repository.GetByIdAsync<Group>(request.GroupId);

			if (group == null)
			{
				throw new GroupNotFoundException(
					string.Format(GroupNotFoundMessage, request.GroupId));
			}

			Message message = new Message()
			{
				Text = request.Text,
				Creator = creator,
				CreatorId = creator.Id,
				Group = group,
				GroupId = group.Id
			};

			await _repository.AddAsync(message);
			await _repository.SaveChangesAsync();

			return await Task.FromResult(message);
		}
	}
}
