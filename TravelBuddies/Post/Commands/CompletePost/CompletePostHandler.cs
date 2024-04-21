namespace TravelBuddies.Application.Post.Commands.CompletePost
{
	using MediatR;
	using System.Threading;
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;

	public class CompletePostHandler : BaseHandler, IRequestHandler<CompletePostCommand, Task>
	{
		public CompletePostHandler(IRepository repository) : base(repository)
		{
		}

		public async Task<Task> Handle(CompletePostCommand request, CancellationToken cancellationToken)
		{
			Post? post = await _repository.GetByIdAsync<Post>(request.PostId);

			if (post == null)
			{
				throw new PostNotFoundException($"Non-extitent Post with Id {request.PostId}");
			}

			if (post.CreatorId != request.CreatorId)
			{
				throw new ApplicationUserNotCreatorException($"User with Id {request.CreatorId} is not creator of post with id {request.PostId}");
			}

			post.IsCompleted = true;
			_repository.Update(post);
			await _repository.SaveChangesAsync();

			return Task.CompletedTask;
		}
	}
}
