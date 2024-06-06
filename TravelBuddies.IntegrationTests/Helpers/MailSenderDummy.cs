namespace TravelBuddies.IntegrationTests.Helpers
{
	using System.Threading.Tasks;
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

		public Task SendMessage(string subject, string body, List<string> recivers)
		{
			return Task.FromResult("");
		}
	}
}
