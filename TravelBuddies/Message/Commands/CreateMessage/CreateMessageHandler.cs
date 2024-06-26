﻿namespace TravelBuddies.Application.Message.Commands.CreateMessage
{
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System.Threading;
    using System.Threading.Tasks;
    using TravelBuddies.Domain.Entities;
    using static TravelBuddies.Application.Common.Exceptions.Messages.ExceptionMessages;
    using TravelBuddies.Application.Common.Interfaces.Repository;
	using TravelBuddies.Application.Common.Exceptions.NotFound;
	using TravelBuddies.Application.Common.Exceptions.Forbidden;

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
			ApplicationUser? creator = await _userManager
				.FindByIdAsync(request.CreatorId);

			if (creator == null)
			{
				throw new ApplicationUserNotFoundException(
					string.Format(ApplicationUserNotFoundMessage, request.CreatorId));
			}

			Group? group = await _repository
				.FirstOrDefaultAsync<Group>(g => g.Id == request.GroupId);

			if (group == null)
			{
				throw new GroupNotFoundException(
					string.Format(GroupNotFoundMessage, request.GroupId));
			}

			UserGroup? userGroup = await _repository
				.FirstOrDefaultAsync<UserGroup>(u => u.UserId == creator.Id);

			if(userGroup == null)
			{
				throw new ApplicationUserNotInGroupException(
					string.Format(ApplicationUserNotInGroupMessage, request.CreatorId, request.GroupId));
			}

			Message message = new Message()
			{
				Text = request.Text,
				Creator = creator,
				CreatorId = creator.Id,
				Group = group,
				GroupId = group.Id,
				CreatedOn = DateTime.Now
			};

			await _repository.AddAsync(message);
			await _repository.SaveChangesAsync();

			return await Task.FromResult(message);
		}
	}
}
