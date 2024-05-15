namespace TravelBuddies.Infrastructure.ExternalVendors.MailSender
{
    using System.Net.Mail;
    using System.Net;
    using TravelBuddies.Application.Common.Interfaces.MailSender;

    public class MailSender : IMailSender
	{
		public void SendMessage(string subject, string body, string reciverEmail)
		{
			string password = "tdjo oqqu fbbk vhpp";
			string emailSender = "travelbuddies.amdaris@gmail.com";

			using SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
			smtpClient.EnableSsl = true;
			smtpClient.UseDefaultCredentials = false;
			smtpClient.Credentials = new NetworkCredential(emailSender, password);

			using MailMessage mailMessage = new MailMessage();
			mailMessage.From = new MailAddress(emailSender);
			mailMessage.To.Add(reciverEmail);
			mailMessage.Subject = subject;
			mailMessage.Body = body;

			smtpClient.SendMailAsync(mailMessage).Wait();
		}
	}
}
