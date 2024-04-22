namespace TravelBuddies.Application.Logger.Commands.CreateLog
{
	using MediatR;
	using System.Threading;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;

	public class CreateLogHandler : BaseHandler, IRequestHandler<CreateLogCommand, Task>
	{
		public CreateLogHandler(IRepository repository) 
			: base(repository)
		{
		}

		public async Task<Task> Handle(CreateLogCommand request, CancellationToken cancellationToken)
		{
			Log log = new Log
			{
				Message = request.Message,
				CreatedOn = DateTime.Now,
				LogLevel = request.Level,
			};

			await _repository.AddAsync(log);
			await _repository.SaveChangesAsync();

			return Task.CompletedTask;
		}
	}
}
