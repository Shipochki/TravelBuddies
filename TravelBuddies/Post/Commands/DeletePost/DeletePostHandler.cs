namespace TravelBuddies.Application.Post.Commands.DeletePost
{
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using System.Threading;
    using TravelBuddies.Domain.Common;
    using TravelBuddies.Domain.Entities;
    using static TravelBuddies.Application.Common.Exceptions.Messages.ExceptionMessages;
    using TravelBuddies.Application.Common.Interfaces.Repository;
	using TravelBuddies.Application.Common.Exceptions.NotFound;
	using TravelBuddies.Application.Common.Exceptions.Forbidden;

	public class DeletePostHandler : BaseHandler, IRequestHandler<DeletePostCommand, int>
	{
		public DeletePostHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager)
			: base(repository, userManager, roleManager)
		{
		}

		public async Task<int> Handle(DeletePostCommand request, CancellationToken cancellationToken)
		{
			Post? post = await _repository
				.GetByIdAsync<Post>(request.PostId);

			if (post == null)
			{
				throw new PostNotFoundException(
					string.Format(PostNotFoundMessage, request.PostId));
			}

			ApplicationUser? user = await _userManager
				.FindByIdAsync(request.CreatorId);

			if (user == null)
			{
				throw new ApplicationUserNotFoundException(
					string.Format(ApplicationUserNotFoundMessage, request.CreatorId));
			}

			if (post.CreatorId != request.CreatorId 
				&& !await _userManager.IsInRoleAsync(user, ApplicationRoles.Admin))
			{
				throw new ApplicationUserNotCreatorException(
					string.Format(ApplicationUserNotCreatorMessage, request.CreatorId));
			}

			post.IsDeleted = true;
			post.DeletedOn = DateTime.Now;

			await _repository.SaveChangesAsync();

			return await Task.FromResult(post.GroupId ?? 0);
		}
	}
}
