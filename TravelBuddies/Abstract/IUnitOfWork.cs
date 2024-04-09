namespace TravelBuddies.Application.Abstract
{
	public interface IUnitOfWork
	{
		public IGroupRepository GroupRepository { get; }

		public ILoggerRepository LoggerRepository { get; }

		public IMessageRepository MessageRepository { get; }

		public IPostRepository PostRepository { get; }

		public IReviewRepository ReviewRepository { get; }

		public IRoleRepository RoleRepository { get; }

		public IUserGroupRepository UserGroupRepository { get; }

		public IUserRepository UserRepository { get; }

		public IUserSubscriptionRepository UserSubscriptionRepository { get; }

		public IVehicleRepository VehicleRepository { get; }

		public IVerificationEmailRepository VerificationEmailRepository { get; }

		Task Save();
	}
}
