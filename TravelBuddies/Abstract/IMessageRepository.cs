namespace TravelBuddies.Application.Abstract
{
	using TravelBuddies.Domain.Entities;

	public interface IMessageRepository
	{
		Task<Message> CreateMessage(Message message);

		Task<Message> EditMessage(Message message);

		Task DeleteMessage(Message message);

		Task<List<Message>> GetAllMessagesByGroupId(int groupId);

		Task<Message?> GetMessageById(int messageId);
	}
}
