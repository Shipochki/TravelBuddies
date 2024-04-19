namespace TravelBuddies.Application.Group.Commands.DeleteGroup
{
	using MediatR;
	using System.Threading;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;

	public class DeleteGroupHandler : BaseHandler, IRequestHandler<DeleteGroupCommand, Task>
	{
		public DeleteGroupHandler(IRepository repository) 
			: base(repository)
		{
		}

		public async Task<Task> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
		{
			Group? group = await _repository.GetByIdAsync<Group>(request.Id);

			if (group == null)
			{
				throw new ArgumentNullException($"Non-extitent Group with Id {request.CreatorId}");
			}

			if (group.CreatorId != request.CreatorId)
			{
				throw new ArgumentException($"User with Id {request.CreatorId} is not creator of group");
			}

			group.IsDeleted = true;
			group.DeletedOn = DateTime.Now;

			return Task.CompletedTask;
		}
	}
}
