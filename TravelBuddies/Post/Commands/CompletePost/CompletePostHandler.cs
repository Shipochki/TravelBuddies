namespace TravelBuddies.Application.Post.Commands.CompletePost
{
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using System.Threading;
    using TravelBuddies.Application.Exceptions;
    using TravelBuddies.Application.Repository;
    using TravelBuddies.Domain.Entities;
    using static TravelBuddies.Application.Exceptions.Messages.ExceptionMessages;

    public class CompletePostHandler : BaseHandler, IRequestHandler<CompletePostCommand, Task>
	{
		public CompletePostHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager)
			: base(repository, userManager, roleManager)
		{
		}

		public async Task<Task> Handle(CompletePostCommand request, CancellationToken cancellationToken)
		{
			Post? post = await _repository.GetByIdAsync<Post>(request.PostId);

			if (post == null)
			{
				throw new PostNotFoundException(
					string.Format(PostNotFoundMessage, request.PostId));
			}

			if (post.CreatorId != request.CreatorId)
			{
				throw new ApplicationUserNotCreatorException(
					string.Format(ApplicationUserNotCreatorMessage, request.CreatorId));
			}

			post.IsCompleted = true;
			await _repository.SaveChangesAsync();

			return Task.CompletedTask;
		}
	}
}
