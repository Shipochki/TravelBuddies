namespace TravelBuddies.Application.Abstract
{
	using TravelBuddies.Domain.Entities;

	public interface IVerificationEmailRepository
	{
		Task CreateVerificationEmail(VerificationEmail verificationEmail);
		Task<VerificationEmail?> GetVerifyEmailByUserId(int userId);
		Task VerifyEmail(VerificationEmail verificationEmail, string code);
	}
}
