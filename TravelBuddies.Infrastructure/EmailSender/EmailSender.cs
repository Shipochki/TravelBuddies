namespace TravelBuddies.Infrastructure.EmailSender
{
	using Microsoft.AspNetCore.Identity.UI.Services;
	using System.Threading.Tasks;

	public class EmailSender : IEmailSender
	{
		public Task SendEmailAsync(string email, string subject, string htmlMessage)
		{
			throw new NotImplementedException();
		}
	}
}
