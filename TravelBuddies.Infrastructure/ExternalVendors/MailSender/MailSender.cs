namespace TravelBuddies.Infrastructure.ExternalVendors.MailSender
{
	using System.Net.Mail;
	using System.Net;
	using TravelBuddies.Application.Common.Interfaces.MailSender;
	using static System.Net.Mime.MediaTypeNames;

	public class MailSender : IMailSender
	{
		public async Task SendMessage(string subject, string body, List<string> recivers)
		{
			string password = "tdjo oqqu fbbk vhpp";
			string emailSender = "travelbuddies.amdaris@gmail.com";

			using SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
			smtpClient.EnableSsl = true;
			smtpClient.UseDefaultCredentials = false;
			smtpClient.Credentials = new NetworkCredential(emailSender, password);

			using MailMessage mailMessage = new MailMessage();
			mailMessage.From = new MailAddress(emailSender);
			recivers.ForEach(r => mailMessage.To.Add(new MailAddress(r)));
			mailMessage.Subject = subject;
			mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;

			await smtpClient.SendMailAsync(mailMessage);
		}
	}
}
