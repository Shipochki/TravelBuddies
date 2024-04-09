namespace TravelBuddies.Infrastructure.Repository
{
	using System.Threading.Tasks;
	using TravelBuddies.Application.Abstract;
	using TravelBuddies.Domain.Entities;

	public class LoggerRepository : ILoggerRepository
	{
		private readonly TravelBuddiesDbContext _context;

        public LoggerRepository(TravelBuddiesDbContext context)
        {
            _context = context;
        }

        public async Task Log(Log log)
		{
			await _context.AddAsync(log);
		}
	}
}
