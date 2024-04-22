namespace TravelBuddies.Application
{
    using TravelBuddies.Application.Repository;

	public abstract class BaseHandler
	{
        protected readonly IRepository _repository;

        protected BaseHandler(IRepository repository)
        {
            _repository = repository;
        }
    }
}
