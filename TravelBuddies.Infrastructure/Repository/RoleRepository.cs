namespace TravelBuddies.Infrastructure.Repository
{
	using Microsoft.EntityFrameworkCore;
	using TravelBuddies.Application.Abstract;
	using TravelBuddies.Domain.Entities;

	public class RoleRepository : IRoleRepository
	{
		private readonly TravelBuddiesDbContext _context;

        public RoleRepository(TravelBuddiesDbContext context)
        {
            _context = context;
        }

        public async Task<Role?> GetRoleByIdAsync(int roleId)
		{
			return await _context
				.Roles
				.FirstOrDefaultAsync(r => r.Id == roleId);
		}

		public async Task<Role?> GetRoleByNameAsync(string name)
		{
			return await _context
				.Roles
				.FirstOrDefaultAsync(r => r.Name == name);
		}
	}
}
