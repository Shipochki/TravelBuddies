namespace TravelBuddies.Application.Message.Commands.UpdateMessage
{
	using MediatR;
	using Microsoft.AspNetCore.Identity;
	using System.Threading;
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;
	using static TravelBuddies.Application.Exceptions.ExceptionMessages;

	public class UpdateMessageHandler : BaseHandler, IRequestHandler<UpdateMessageCommand, Task>
	{
		public UpdateMessageHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager)
			: base(repository, userManager, roleManager)
		{
		}

		public async Task<Task> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
		{
			Message? message = await _repository.GetByIdAsync<Message>(request.Id);

			if (message == null)
			{
				throw new MessageNotFoundException(string.Format(MessageNotFoundMessage, request.Id));
			}

			if (message.CreatorId != request.CreatorId)
			{
				throw new ApplicationUserNotCreatorException(
					string.Format(ApplicationUserNotCreatorMessage, request.CreatorId));
			}

			if(message.GroupId != request.GroupId)
			{
				throw new GroupNotMatchException(GroupNotMatchMessage);
			}

			message.Text = request.Text;

			_repository.Update(message);
			await _repository.SaveChangesAsync();

			return Task.CompletedTask;
		}
	}
}
