namespace TravelBuddies.Application.Abstract
{
	using TravelBuddies.Domain.Entities;

	public interface ILoggerRepository
	{
		Task Log(Log log);
	}
}
