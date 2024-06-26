﻿namespace TravelBuddies.Application.Message.Commands.UpdateMessage
{
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using System.Threading;
    using TravelBuddies.Domain.Entities;
    using static TravelBuddies.Application.Common.Exceptions.Messages.ExceptionMessages;
    using TravelBuddies.Application.Common.Interfaces.Repository;
	using TravelBuddies.Application.Common.Exceptions.NotFound;
	using TravelBuddies.Application.Common.Exceptions.Forbidden;
	using TravelBuddies.Application.Common.Exceptions.BadRequest;

	public class UpdateMessageHandler : BaseHandler, IRequestHandler<UpdateMessageCommand, Task>
	{
		public UpdateMessageHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager)
			: base(repository, userManager, roleManager)
		{
		}

		public async Task<Task> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
		{
			Message? message = await _repository
				.GetByIdAsync<Message>(request.Id);

			if (message == null)
			{
				throw new MessageNotFoundException(
					string.Format(MessageNotFoundMessage, request.Id));
			}

			if (message.CreatorId != request.CreatorId)
			{
				throw new ApplicationUserNotCreatorException(
					string.Format(ApplicationUserNotCreatorMessage, request.CreatorId));
			}

			if(message.GroupId != request.GroupId)
			{
				throw new GroupNotMatchException(GroupNotMatchMessage);
			}

			message.Text = request.Text;
			message.UpdatedOn = DateTime.Now;

			await _repository.SaveChangesAsync();

			return Task.CompletedTask;
		}
	}
}
