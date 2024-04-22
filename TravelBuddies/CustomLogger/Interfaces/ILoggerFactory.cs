namespace TravelBuddies.Application.CustomLogger.Interfaces
{
	using MediatR;

	public interface ILoggerFactory
	{
		public FileLogger CreateFileLoggerAsync(string categoryName);

		public DatabaseLogger CreateDatabaseLoggerAsync();
	}
}
