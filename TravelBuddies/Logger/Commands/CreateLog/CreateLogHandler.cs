namespace TravelBuddies.Application.Logger.Commands.CreateLog
{
	using MediatR;
	using System.Threading;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;

	public class CreateLogHandler : IRequestHandler<CreateLogCommand, Task>
	{
		private readonly IRepository _repository;

        public CreateLogHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Task> Handle(CreateLogCommand request, CancellationToken cancellationToken)
		{
			Log log = new Log
			{
				Message = request.Message,
				CreatedOn = DateTime.Now,
			};

			await _repository.AddAsync(log);
			await _repository.SaveChangesAsync();

			return Task.CompletedTask;
		}
	}
}
