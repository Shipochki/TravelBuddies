namespace TravelBuddies.Application.Common.Interfaces.MailSender
{
    public interface IMailSender
    {
        public Task SendMessage(string subject, string body, List<string> recivers);

	}
}
