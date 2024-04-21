namespace TravelBuddies.Application.Post.Commands.UpdatePostGroup
{
	using MediatR;
	using System.Threading;
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;

	public class UpdatePostGroupHandler : BaseHandler, IRequestHandler<UpdatePostGroupCommand, Task>
	{
		public UpdatePostGroupHandler(IRepository repository) 
			: base(repository)
		{
		}

		public async Task<Task> Handle(UpdatePostGroupCommand request, CancellationToken cancellationToken)
		{
			Post? post = await _repository.GetByIdAsync<Post>(request.PostId);

			if (post == null)
			{
				throw new PostNotFoundException($"Non-extitent Post with Id {request.PostId}");
			}

			Group? group = await _repository.GetByIdAsync<Group>(request.GroupId);

			if (group == null)
			{
				throw new GroupNotFoundException($"Non-extitent Group with Id {request.GroupId}");
			}

			post.Group = group;
			post.GroupId = group.Id;

			_repository.Update(post);
			await _repository.SaveChangesAsync();
			return Task.CompletedTask;
		}
	}
}
