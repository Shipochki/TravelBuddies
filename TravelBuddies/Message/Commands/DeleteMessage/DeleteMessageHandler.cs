namespace TravelBuddies.Application.Message.Commands.DeleteMessage
{
	using MediatR;
	using System.Threading;
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;

	public class DeleteMessageHandler : BaseHandler, IRequestHandler<DeleteMessageCommand, Task>
	{
		public DeleteMessageHandler(IRepository repository) 
			: base(repository)
		{
		}

		public async Task<Task> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
		{
			Message? message = await _repository.GetByIdAsync<Message>(request.MessageId);

			if (message == null)
			{
				throw new MessageNotFoundException($"Non-extitent Message with Id {request.MessageId}");
			}

			if (message.CreatorId != request.CreatorId)
			{
				throw new ApplicationUserNotCreatorException($"User with Id {request.CreatorId} is not creator of message");
			}

			message.IsDeleted = true;

			_repository.Update(message);
			await _repository.SaveChangesAsync();

			return Task.CompletedTask;
		}
	}
}
