namespace TravelBuddies.Application.Message.Commands.DeleteMessage
{
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using System.Threading;
    using TravelBuddies.Domain.Common;
    using TravelBuddies.Application.Exceptions;
    using TravelBuddies.Application.Repository;
    using TravelBuddies.Domain.Entities;
    using static TravelBuddies.Application.Exceptions.Messages.ExceptionMessages;

    public class DeleteMessageHandler : BaseHandler, IRequestHandler<DeleteMessageCommand, Task>
	{
		public DeleteMessageHandler(IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager)
			: base(repository, userManager, roleManager)
		{
		}

		public async Task<Task> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
		{
			Message? message = await _repository.GetByIdAsync<Message>(request.MessageId);

			if (message == null)
			{
				throw new MessageNotFoundException(
					string.Format(MessageNotFoundMessage, request.MessageId));
			}

			ApplicationUser? user = await _userManager.FindByIdAsync(request.CreatorId);

			if (user == null)
			{
				throw new ApplicationUserNotFoundException(
					string.Format(ApplicationUserNotFoundMessage, request.CreatorId));
			}

			if (message.CreatorId != request.CreatorId 
				&& !await _userManager.IsInRoleAsync(user, ApplicationRoles.Admin))
			{
				throw new ApplicationUserNotCreatorException(
					string.Format(ApplicationUserNotCreatorMessage, request.CreatorId));
			}

			message.IsDeleted = true;

			_repository.Update(message);
			await _repository.SaveChangesAsync();

			return Task.CompletedTask;
		}
	}
}
