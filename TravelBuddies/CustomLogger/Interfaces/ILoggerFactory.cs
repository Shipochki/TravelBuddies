namespace TravelBuddies.Application.CustomLogger.Interfaces
{
	using MediatR;

	public interface ILoggerFactory
	{
		public Task<FileLogger> CreateFileLoggerAsync(string categoryName);

		public Task<DatabaseLogger> CreateDatabaseLoggerAsync(IMediator mediator);
	}
}
