namespace TravelBuddies.IntegrationTests.Helpers
{
	using Microsoft.EntityFrameworkCore;
	using TravelBuddies.Infrastructure;

	public class DataContextBuilder : IDisposable
	{

		private readonly TravelBuddiesDbContext _dataContext;

        public DataContextBuilder(string dbName = "TestDatabase")
        {
            var options = new DbContextOptionsBuilder<TravelBuddiesDbContext>()
				.UseInMemoryDatabase(databaseName: dbName)
				.Options;

			var context = new TravelBuddiesDbContext(options);

			_dataContext = context;
        }

        public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
            if (disposing)
            {
				_dataContext.Dispose();
            }
        }
	}
}
