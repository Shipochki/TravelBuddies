namespace TravelBuddies.Application.Message.Commands.CreateMessage
{
	using MediatR;
	using System.Threading;
	using System.Threading.Tasks;
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;

	public class CreateMessageHandler : BaseHandler, IRequestHandler<CreateMessageCommand, Message>
	{
		public CreateMessageHandler(IRepository repository) 
			: base(repository)
		{
		}

		public async Task<Message> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
		{
			ApplicationUser? creator = await _repository.GetByIdAsync<ApplicationUser>(request.CreatorId);

			if (creator == null)
			{
				throw new ApplicationUserNotFoundException($"Non-extitent User with Id {request.CreatorId}");
			}

			Group? group = await _repository.GetByIdAsync<Group>(request.GroupId);

			if (group == null)
			{
				throw new GroupNotFoundException($"Non-extitent Group with Id {request.GroupId}");
			}

			Message message = new Message()
			{
				Text = request.Text,
				Creator = creator,
				CreatorId = creator.Id,
				Group = group,
				GroupId = group.Id
			};

			await _repository.AddAsync(message);
			await _repository.SaveChangesAsync();

			return await Task.FromResult(message);
		}
	}
}
