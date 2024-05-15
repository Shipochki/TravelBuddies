namespace TravelBuddies.Application.Common.Interfaces.MailSender
{
    public interface IMailSender
    {
        public void SendMessage(string subject, string body, string reciverEmail);
    }
}
