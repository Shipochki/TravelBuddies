﻿namespace TravelBuddies.Application.Message.Queries.GetMessagesByGroupId
{
	using MediatR;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.EntityFrameworkCore;
	using System.Threading;
	using System.Threading.Tasks;
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;
	using static TravelBuddies.Application.Exceptions.ExceptionMessages;

	public class GetMessagesByGroupIdHandler : BaseHandler, IRequestHandler<GetMessagesByGroupIdQuery, IEnumerable<Message>>
	{
		public GetMessagesByGroupIdHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager)
			: base(repository, userManager, roleManager)
		{
		}

		public async Task<IEnumerable<Message>> Handle(GetMessagesByGroupIdQuery request, CancellationToken cancellationToken)
		{
			ApplicationUser? user = await _userManager.FindByIdAsync(request.UserId);

			if (user == null)
			{
				throw new ApplicationUserNotFoundException(string.Format(ApplicationUserNotFoundMessage, request.UserId));
			}

			Group? group = await _repository.GetByIdAsync<Group>(request.GroupId);

			if (group == null)
			{
				throw new GroupNotFoundException(string.Format(GroupNotFoundMessage, request.GroupId));
			}

			UserGroup? userGroup = await _repository
				.FirstOrDefaultAsync<UserGroup>(u => u.UserId == request.UserId && u.GroupId == request.GroupId);

			if(userGroup == null)
			{
				throw new ApplicationUserNotInGroupException(string.Format(ApplicationUserNotInGroupMessage, request.UserId, request.GroupId));
			}

			List<Message> messages = await _repository
				.All<Message>(m => m.IsDeleted == false && m.GroupId == group.Id)
				.ToListAsync();

			return await Task.FromResult(messages);
		}
	}
}