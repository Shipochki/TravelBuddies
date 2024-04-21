namespace TravelBuddies.Application.Message.Queries.GetMessagesByGroupId
{
	using MediatR;
	using Microsoft.EntityFrameworkCore;
	using System.Threading;
	using System.Threading.Tasks;
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;

	public class GetMessagesByGroupIdHandler : BaseHandler, IRequestHandler<GetMessagesByGroupIdQuery, IEnumerable<Message>>
	{
		public GetMessagesByGroupIdHandler(IRepository repository) 
			: base(repository)
		{
		}

		public async Task<IEnumerable<Message>> Handle(GetMessagesByGroupIdQuery request, CancellationToken cancellationToken)
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

			UserGroup? userGroup = await _repository.FirstOrDefaultAsync<UserGroup>(u => u.UserId == request.UserId && u.GroupId == request.GroupId);

			if(userGroup == null)
			{
				throw new ApplicationUserNotInGroupException($"User with Id {user.Id} is not in group with Id {group.Id}");
			}

			List<Message> messages = await _repository
				.All<Message>(m => m.IsDeleted == false && m.GroupId == group.Id)
				.ToListAsync();

			return await Task.FromResult(messages);
		}
	}
}
