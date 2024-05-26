namespace TravelBuddies.IntegrationTests.Helpers
{
	using TravelBuddies.Application.Common.Interfaces.MailSender;

	internal class MailSenderDummy : IMailSender
	{
		public string GenerateRegistrationEmailMessage(string userName)
		{
			return "";
		}

		public string GenretateCompletePostMessage(string userName, string paymentLink)
		{
			return "";
		}

		public void SendMessage(string subject, string body, string reciverEmail)
		{
			return;
		}
	}
}
