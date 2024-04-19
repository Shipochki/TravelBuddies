using TravelBuddies.Application.Repository;

namespace TravelBuddies.Application
{
	public abstract class BaseHandler
	{
		protected IRepository _repository { get; set; }

        protected BaseHandler(IRepository repository)
        {
            _repository = repository;
        }
    }
}
