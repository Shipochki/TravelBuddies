﻿namespace TravelBuddies.Application.Post.Commands.DeletePost
{
	using MediatR;
	using Microsoft.AspNetCore.Identity;
	using System.Threading;
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;
	using static TravelBuddies.Application.Exceptions.ExceptionMessages;

	public class DeletePostHandler : BaseHandler, IRequestHandler<DeletePostCommand, Task>
	{
		public DeletePostHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager)
			: base(repository, userManager, roleManager)
		{
		}

		public async Task<Task> Handle(DeletePostCommand request, CancellationToken cancellationToken)
		{
			Post? post = await _repository.GetByIdAsync<Post>(request.PostId);

			if (post == null)
			{
				throw new PostNotFoundException(string.Format(PostNotFoundMessage, request.PostId));
			}

			if (post.CreatorId != request.CreatorId)
			{
				throw new ApplicationUserNotCreatorException(string.Format(ApplicationUserNotCreatorMessage, request.CreatorId));
			}

			post.IsDeleted = true;
			post.DeletedOn = DateTime.Now;

			_repository.Update(post);
			await _repository.SaveChangesAsync();

			return Task.CompletedTask;
		}
	}
}