namespace TravelBuddies.Application.Logger.Commands.AddLog
{
	using MediatR;
	using System.Threading;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;

	public class AddLogHandler : IRequestHandler<AddLogCommand, Task>
	{
		private readonly IRepository _repository;

        public AddLogHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Task> Handle(AddLogCommand request, CancellationToken cancellationToken)
		{
			Log log = new Log
			{
				Message = request.Message,
				CreatedOn = DateTime.Now,
			};

			await _repository.AddAsync(log);
			return Task.CompletedTask;
		}
	}
}
