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

		public const string JoinNotificationToDriver = @"
    <html>
    <body>
        <div class=""container"">
            <div class=""header"">
                New Member Joined Your Group
            </div>
            <div class=""message"">
                Hi <strong>{{driver_name}}</strong>,
                <br><br>
                We're pleased to inform you that a new member, <strong>{{member_name}}</strong>, has joined your group ""{{group_name}}"". They are eager to contribute and collaborate with you.
                <br><br>
                Please welcome them to the group and feel free to reach out if you need any assistance.
                <br><br>
                Best regards,
                <br>
                Your Group Management Team
            </div>
        </div>
    </body>
    </html>
    ";

		public const string LeaveNotificationToDriver = @"
    <html>
    <body>
        <div class=""container"">
            <div class=""header"">
                Member Left Your Group
            </div>
            <div class=""message"">
                Hi <strong>{{driver_name}}</strong>,
                <br><br>
                We regret to inform you that <strong>{{member_name}}</strong> has left your group ""{{group_name}}"". Their contributions will be missed.
                <br><br>
                If you have any questions or need assistance regarding your group, please feel free to reach out to us.
                <br><br>
                Best regards,
                <br>
                Your Group Management Team
            </div>
        </div>
    </body>
    </html>
    ";

		public const string JoinNotificationToMember = @"
    <html>
    <body>
        <div class=""container"">
            <div class=""header"">
                Welcome to the Group!
            </div>
            <div class=""message"">
                Hi <strong>{{member_name}}</strong>,
                <br><br>
                Welcome to the ""{{group_name}}""! We're excited to have you join us. Your participation is greatly appreciated.
                <br><br>
                If you have any questions or need assistance, please don't hesitate to contact us.
                <br><br>
                Best regards,
                <br>
                The Group Team
            </div>
        </div>
    </body>
    </html>
    ";

		public const string LeaveNotificationToMember = @"
    <html>
    <body>
        <div class=""container"">
            <div class=""header"">
                You've Left the Group
            </div>
            <div class=""message"">
                Hi <strong>{{member_name}}</strong>,
                <br><br>
                We're sorry to inform you that you have left the ""{{group_name}}"". We appreciate your participation during your time with us.
                <br><br>
                If you have any questions or would like further information, please feel free to reach out.
                <br><br>
                Best regards,
                <br>
                The Group Team
            </div>
        </div>
    </body>
    </html>
    ";
	}
}
