namespace TravelBuddies.Application.UserGroup.Commands.DeleteUserGroup
{
	using MediatR;
	using System.Threading;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;

	public class DeleteUserGroupHandler : BaseHandler, IRequestHandler<DeleteUserGroupCommand, Task>
	{
		public DeleteUserGroupHandler(IRepository repository) : base(repository)
		{
		}

		public async Task<Task> Handle(DeleteUserGroupCommand request, CancellationToken cancellationToken)
		{
			UserGroup? userGroup = await _repository
				.FirstOrDefaultAsync<UserGroup>(u => u.GroupId == request.GroupId && u.UserId == request.UserId);

			if(userGroup == null)
			{
				throw new ArgumentNullException("Invalid UserGroup input");
			}

			_repository.Delete(userGroup);
			await _repository.SaveChangesAsync();
			return Task.CompletedTask;
		}
	}
}
