﻿namespace TravelBuddies.Application.Post.Commands.CompletePost
{
	using MediatR;
	using Microsoft.AspNetCore.Identity;
	using System.Threading;
	using TravelBuddies.Domain.Entities;
	using static TravelBuddies.Application.Common.Exceptions.Messages.ExceptionMessages;
	using TravelBuddies.Application.Common.Interfaces.Repository;
	using TravelBuddies.Application.Common.Exceptions.NotFound;
	using TravelBuddies.Application.Common.Exceptions.Forbidden;
	using TravelBuddies.Application.Common.Interfaces.MailSender;
	using Microsoft.EntityFrameworkCore;
	using TravelBuddies.Domain.Enums;

	public class CompletePostHandler : BaseHandler, IRequestHandler<CompletePostCommand, Task>
	{
		private readonly IMailSender _mailSender;

		public CompletePostHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager
			, IMailSender mailSender)
			: base(repository, userManager, roleManager)
		{
			_mailSender = mailSender;
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

			if (post.PaymentType == PaymentType.CashAndCard || post.PaymentType == PaymentType.Card)
			{
				await SendMessageToAllMembers(post);
			}

			post.IsCompleted = true;
			await _repository.SaveChangesAsync();

			return Task.CompletedTask;
		}

		private async Task SendMessageToAllMembers(Post post)
		{
			Group? group = await _repository
							.All<Group>(g => g.Id == post.GroupId)
							.Include(g => g.UsersGroups)
							.ThenInclude(g => g.User)
							.FirstOrDefaultAsync();

			if (group == null)
			{
				throw new GroupNotFoundException(
					string.Format(GroupNotFoundMessage, post.GroupId));
			}

			for (int i = 0; i < group.UsersGroups.Count; i++)
			{
				UserGroup userGroup = group.UsersGroups[i];

				if(userGroup.UserId != group.CreatorId)
				{
					string body = _mailSender.GenretateCompletePostMessage($"{userGroup.User.FirstName} {userGroup.User.LastName}", post.PaymentLink);
					_mailSender.SendMessage("Complete trip", body, userGroup.User.Email);
				}

			}
		}
	}
}
