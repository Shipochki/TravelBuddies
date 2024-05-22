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

		public string GenerateRegistrationEmail(string userName)
		{
			return $@"
					<html>
					<body>
						<h2>Welcome to TravelBuddies!</h2>
						<p>Hi {userName},</p>
						<p>Thank you for registering at TravelBuddies! We're excited to have you on board.</p>
						<p><a href='https://localhost:5173/login'>Login to your account</a></p>
						<p>If you did not create an account with us, please ignore this email.</p>
						<br />
						<p>Best regards,</p>
						<p>The TravelBuddies Team</p>
					</body>
					</html>";
		}

	}
}
