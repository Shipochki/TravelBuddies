namespace TravelBuddies.Application.Common.MailMessages
{
	public static class MailMessages
	{
		public const string SuccesfullRegisterMessage =
			@"
					<html>
					<body>
						<h2>Welcome to TravelBuddies!</h2>
						<p>Hi {0},</p>
						<p>Thank you for registering at TravelBuddies! We're excited to have you on board.</p>
						<p><a href='https://localhost:5173/login'>Login to your account</a></p>
						<p>If you did not create an account with us, please ignore this email.</p>
						<br />
						<p>Best regards,</p>
						<p>The TravelBuddies Team</p>
					</body>
					</html>";


		public const string CompletePostMessage =
			@"
                    <html lang=""en"">
                        <body>
                            <div class=""container"">
                                <div class=""header"">
                                    <h1>Thank You for Using Our App!</h1>
                                </div>
                                 <div class=""content"">
                                    <p>Hi {1},</p>
                                    <p>Thank you for being an active member of our community! We're excited to let you know that your recent group activity has been completed successfully.</p>
                                    <p>To ensure everything is settled, please take a moment to complete your payment for the trip by clicking the link below:</p>
                                    <p><a href=""{2}"" class=""button"">Complete Your Payment</a></p>
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
