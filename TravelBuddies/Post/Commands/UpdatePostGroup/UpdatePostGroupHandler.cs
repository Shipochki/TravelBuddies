namespace TravelBuddies.Application.Post.Commands.UpdatePostGroup
{
	using MediatR;
	using Microsoft.AspNetCore.Identity;
	using System.Threading;
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;
	using static TravelBuddies.Application.Exceptions.ExceptionMessages;

	public class UpdatePostGroupHandler : BaseHandler, IRequestHandler<UpdatePostGroupCommand, Task>
	{
		public UpdatePostGroupHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager)
			: base(repository, userManager, roleManager)
		{
		}

		public async Task<Task> Handle(UpdatePostGroupCommand request, CancellationToken cancellationToken)
		{
			Post? post = await _repository.GetByIdAsync<Post>(request.PostId);

			if (post == null)
			{
				throw new PostNotFoundException(string.Format(PostNotFoundMessage, request.PostId));
			}

			Group? group = await _repository.GetByIdAsync<Group>(request.GroupId);

			if (group == null)
			{
				throw new GroupNotFoundException(string.Format(GroupNotFoundMessage, request.GroupId));
			}

			post.Group = group;
			post.GroupId = group.Id;

			_repository.Update(post);
			await _repository.SaveChangesAsync();
			return Task.CompletedTask;
		}
	}
}
