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

		public string GenerateRegistrationEmailMessage(string userName)
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

		public string GenretateCompletePostMessage(string userName, string paymentLink)
		{
			return $@"
                    <html lang=""en"">
                    <head>
                        <meta charset=""UTF-8"">
                        <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                        <title>Thank You {userName}</title>
                        <style>
                            body {{
                                font-family: Arial, sans-serif;
                                background-color: #f4f4f4;
                                margin: 0;
                                padding: 0;
                                   }}
                            .container {{
                                width: 100%;
                                max-width: 600px;
                                margin: 0 auto;
                                background-color: #ffffff;
                                padding: 20px;
                                box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                                    }}
                            .header {{
                                text-align: center;
                                padding: 10px 0;
                                    }}
                            .content {{
                                padding: 20px;
                                    }}
                            .button {{
                                display: inline-block;
                                padding: 10px 20px;
                                margin: 20px 0;
                                color: #ffffff;
                                background-color: #28a745;
                                text-decoration: none;
                                border-radius: 5px;
                                    }}
                            .footer {{
                                text-align: center;
                                padding: 10px 0;
                                font-size: 12px;
                                color: #777777;
                                    }}
                        </style>
                    </head>
                    <body>
                        <div class=""container"">
                            <div class=""header"">
                                <h1>Thank You for Using Our App!</h1>
                            </div>
                            <div class=""content"">
                                <p>Hi {userName},</p>
                                <p>Thank you for being an active member of our community! We're excited to let you know that your recent group activity has been completed successfully.</p>
                                <p>To ensure everything is settled, please take a moment to complete your payment for the trip by clicking the link below:</p>
                                <p><a href=""{paymentLink}"" class=""button"">Complete Your Payment</a></p>
                                <p>We appreciate your continued support and trust in our app. If you have any questions or need further assistance, please don't hesitate to reach out to us.</p>
                                <p>Best regards,</p>
                                <p>The TravelBuddies Team</p>
                            </div>
                            <div class=""footer"">
                                <p>&copy; 2024 TravelBuddies. All rights reserved.</p>
                            </div>
                        </div>
                    </body>
                </html>";
		}

	}
}
