namespace TravelBuddies.Infrastructure
{
    using TravelBuddies.Application.Abstract;

    public class UnitOfWork : IUnitOfWork
	{
		private readonly TravelBuddiesDbContext _context;

        public UnitOfWork(TravelBuddiesDbContext context)
        {
            _context = context;
        }

        public async Task Save()
		{
			await _context.SaveChangesAsync();
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
