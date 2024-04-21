namespace TravelBuddies.Application.UserGroup.Commands.CreateUserGroup
{
	using MediatR;
	using Microsoft.EntityFrameworkCore;
	using System.Threading;
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;

	public class CreateUserGroupHandler : BaseHandler, IRequestHandler<CreateUserGroupCommand, Task>
	{
		public CreateUserGroupHandler(IRepository repository) : base(repository)
		{
		}

		public async Task<Task> Handle(CreateUserGroupCommand request, CancellationToken cancellationToken)
		{
			ApplicationUser? user = await _repository.GetByIdAsync<ApplicationUser>(request.UserId);

			if (user == null)
			{
				throw new ApplicationUserNotFoundException($"Non-extitent User with Id {request.UserId}");
			}

			Group? group = await _repository.GetByIdAsync<Group>(request.GroupId);

			if (group == null)
			{
				throw new GroupNotFoundException($"Non-extitent Group with Id {request.GroupId}");
			}

			UserGroup? userGroup = await _repository
				.FirstOrDefaultAsync<UserGroup>(u => u.GroupId == request.GroupId && u.UserId == request.UserId);

			if (userGroup != null)
			{
				throw new ApplicationUserAllreadyInGroupException($"User with Id {user.Id} is allready in Group with Id {group.Id}");
			}

			Post? post = await _repository.GetByIdAsync<Post>(group.PostId);

			List<UserGroup> userGroups = await _repository
				.AllReadonly<UserGroup>(u => u.GroupId == request.GroupId)
				.ToListAsync();

			if(post.FreeSeats == userGroups.Count)
			{
				throw new NotAvailableSeatsInPostException($"No available seats in group with id {request.GroupId}");
			}

			userGroup = new UserGroup()
			{
				User = user,
				UserId = user.Id,
				Group = group,
				GroupId = group.Id
			};

			await _repository.AddAsync(userGroup);
			await _repository.SaveChangesAsync();

			return Task.CompletedTask;
		}
	}
}
