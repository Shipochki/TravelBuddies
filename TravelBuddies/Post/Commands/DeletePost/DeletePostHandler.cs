namespace TravelBuddies.Application.Post.Commands.DeletePost
{
	using MediatR;
	using System.Threading;
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;

	public class DeletePostHandler : BaseHandler, IRequestHandler<DeletePostCommand, Task>
	{
		public DeletePostHandler(IRepository repository) 
			: base(repository)
		{
		}

		public async Task<Task> Handle(DeletePostCommand request, CancellationToken cancellationToken)
		{
			Post? post = await _repository.GetByIdAsync<Post>(request.PostId);

			if (post == null)
			{
				throw new PostNotFoundException($"Non-extitent Post with Id {request.PostId}");
			}

			if (post.CreatorId != request.CreatorId)
			{
				throw new ApplicationUserNotCreatorException($"User with Id {request.CreatorId} is not creator of post");
			}

			post.IsDeleted = true;
			post.DeletedOn = DateTime.Now;

			_repository.Update(post);
			await _repository.SaveChangesAsync();

			return Task.CompletedTask;
		}
	}
}
