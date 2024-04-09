namespace TravelBuddies.Infrastructure.Repository
{
	using Microsoft.EntityFrameworkCore;
	using TravelBuddies.Application.Abstract;
	using TravelBuddies.Domain.Entities;

	public class MessageRepository : IMessageRepository
	{
		private readonly TravelBuddiesDbContext _context;

        public MessageRepository(TravelBuddiesDbContext context)
        {
            _context = context;
        }

        public async Task CreateMessageAsync(Message message)
		{
			await _context.AddAsync(message);
		}

		public void UpdateMessageAsync(Message message)
		{
			_context.Update(message);
		}

		public async Task<List<Message>> GetAllMessagesByGroupId(int groupId)
		{
			return await _context
				.Messages
				.Where(m => m.IsDeleted == false && m.GroupId == groupId)
				.ToListAsync();
		}

		public async Task<Message?> GetMessageById(int messageId)
		{
			return await _context
				.Messages
				.FirstOrDefaultAsync(m => m.IsDeleted == false && m.Id == messageId);
		}
	}
}
