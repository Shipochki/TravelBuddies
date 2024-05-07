namespace TravelBuddies.Application.Interfaces.MailSender
{
	public interface IMailSender
	{
		public void SendMessage(string subject, string body, string reciverEmail);
	}
}
