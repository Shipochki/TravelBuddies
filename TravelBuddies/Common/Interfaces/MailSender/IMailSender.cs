namespace TravelBuddies.Application.Common.Interfaces.MailSender
{
    public interface IMailSender
    {
        public void SendMessage(string subject, string body, string reciverEmail);

        public string GenerateRegistrationEmailMessage(string userName);

        public string GenretateCompletePostMessage(string userName, string paymentLink);

	}
}
