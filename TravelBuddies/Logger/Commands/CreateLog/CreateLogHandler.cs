namespace TravelBuddies.Application.Logger.Commands.CreateLog
{
	using MediatR;
	using Microsoft.AspNetCore.Identity;
	using System.Threading;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;

	public class CreateLogHandler : BaseHandler, IRequestHandler<CreateLogCommand, Task>
	{
		public CreateLogHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager)
			: base(repository, userManager, roleManager)
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
