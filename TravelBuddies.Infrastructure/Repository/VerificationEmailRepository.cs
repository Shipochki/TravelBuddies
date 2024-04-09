namespace TravelBuddies.Infrastructure.Repository
{
	using Microsoft.EntityFrameworkCore;
	using TravelBuddies.Application.Abstract;
	using TravelBuddies.Domain.Entities;

	public class VerificationEmailRepository : IVerificationEmailRepository
	{
		private readonly TravelBuddiesDbContext _context;

        public VerificationEmailRepository(TravelBuddiesDbContext context)
        {
            _context = context;
        }

        public async Task CreateVerificationEmail(VerificationEmail verificationEmail)
		{
			await _context.AddAsync(verificationEmail);
		}

		public async Task<VerificationEmail?> GetVerifyEmailByUserId(int userId)
		{
			return await _context
				.VerificationEmails
				.FirstOrDefaultAsync(r => r.UserId == userId);
		}

		public Task VerifyEmail(VerificationEmail verificationEmail, string code)
		{
			throw new NotImplementedException();
		}
	}
}
