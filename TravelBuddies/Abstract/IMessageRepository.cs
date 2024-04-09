namespace TravelBuddies.Application.Abstract
{
	using TravelBuddies.Domain.Entities;

	public interface IMessageRepository
	{
		Task CreateMessageAsync(Message message);

		void UpdateMessageAsync(Message message);

		Task<List<Message>> GetAllMessagesByGroupId(int groupId);

		Task<Message?> GetMessageById(int messageId);
	}
}
