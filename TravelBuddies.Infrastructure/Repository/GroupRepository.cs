namespace TravelBuddies.Infrastructure.Repository
{
	using Microsoft.EntityFrameworkCore;
	using System.Threading.Tasks;
	using TravelBuddies.Application.Abstract;
	using TravelBuddies.Domain.Entities;

	public class GroupRepository : IGroupRepository
	{
		private readonly TravelBuddiesDbContext _context;

        public GroupRepository(TravelBuddiesDbContext context)
        {
            _context = context;
        }

        public async Task CreateGroupAsync(Group group)
		{
			await _context.AddAsync(group);
		}

		public async Task<Group?> GetGroupById(int groupId)
		{
			return await _context
				.Groups
				.FirstOrDefaultAsync(g => g.IsDeleted == false && g.Id == groupId);
		}

		public async Task<Group?> GetGroupByPostId(int postId)
		{
			return await _context
				.Groups
				.FirstOrDefaultAsync(g => g.IsDeleted == false && g.PostId == postId);
		}
	}
}
