namespace TravelBuddies.Infrastructure
{
    using TravelBuddies.Application.Abstract;

    public class UnitOfWork : IUnitOfWork
	{
		private readonly TravelBuddiesDbContext _context;

        public UnitOfWork(
			TravelBuddiesDbContext context,
			IGroupRepository groupRepository,
			ILoggerRepository loggerRepository,
			IMessageRepository messageRepository,
			IPostRepository postRepository,
			IReviewRepository reviewRepository,
			IRoleRepository roleRepository,
			IUserGroupRepository userGroupRepository,
			IUserRepository userRepository,
			IUserSubscriptionRepository userSubscriptionRepository,
			IVehicleRepository vehicleRepository,
			IVerificationEmailRepository verificationEmailRepository)
        {
            _context = context;
			GroupRepository = groupRepository;
			LoggerRepository = loggerRepository;
			MessageRepository = messageRepository;
			PostRepository = postRepository;
			ReviewRepository = reviewRepository;
			RoleRepository = roleRepository;
			UserGroupRepository = userGroupRepository;
			UserRepository = userRepository;
			UserSubscriptionRepository = userSubscriptionRepository;
			VehicleRepository = vehicleRepository;
			VerificationEmailRepository = verificationEmailRepository;
        }

        public IGroupRepository GroupRepository { get; private set; }

		public ILoggerRepository LoggerRepository { get; private set; }

		public IMessageRepository MessageRepository { get; private set; }

		public IPostRepository PostRepository { get; private set; }

		public IReviewRepository ReviewRepository { get; private set; }

		public IRoleRepository RoleRepository { get; private set; }

		public IUserGroupRepository UserGroupRepository { get; private set; }

		public IUserRepository UserRepository { get; private set; }

		public IUserSubscriptionRepository UserSubscriptionRepository { get; private set; }

		public IVehicleRepository VehicleRepository { get; private set; }

		public IVerificationEmailRepository VerificationEmailRepository { get; private set; }

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
