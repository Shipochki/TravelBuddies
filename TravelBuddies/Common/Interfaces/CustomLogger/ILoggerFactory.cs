namespace TravelBuddies.Application.Common.Interfaces.CustomLogger
{
    public interface ILoggerFactory
    {
        public ILogger CreateFileLoggerAsync(string categoryName);

        public ILogger CreateDatabaseLoggerAsync();
    }
}
