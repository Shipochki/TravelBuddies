namespace TravelBuddies.Application.Message.Commands.UpdateMessage
{
	using MediatR;
	using System.Threading;
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;

	public class UpdateMessageHandler : BaseHandler, IRequestHandler<UpdateMessageCommand, Task>
	{
		public UpdateMessageHandler(IRepository repository) 
			: base(repository)
		{
		}

		public async Task<Task> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
		{
			Message? message = await _repository.GetByIdAsync<Message>(request.Id);

			if (message == null)
			{
				throw new MessageNotFoundException($"Non-extitent Message with Id {request.Id}");
			}

			if (message.CreatorId != request.CreatorId)
			{
				throw new ApplicationUserNotCreatorException($"User with Id {request.CreatorId} is not creator of message");
			}

			if(message.GroupId != request.GroupId)
			{
				throw new GroupNotMatchException("Message group is not matching");
			}

			message.Text = request.Text;

			_repository.Update(message);
			await _repository.SaveChangesAsync();

			return Task.CompletedTask;
		}
	}
}
