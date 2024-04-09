namespace TravelBuddies.Infrastructure.Repository
{
	using Microsoft.EntityFrameworkCore;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using TravelBuddies.Application.Abstract;
	using TravelBuddies.Domain.Entities;

	public class PostRepository : IPostRepository
	{
		private readonly TravelBuddiesDbContext _context;

        public PostRepository(TravelBuddiesDbContext context)
        {
            _context = context;
        }

		public async Task CreatePostAsync(Post post)
		{
			await _context.AddAsync(post);
		}

		public async Task<List<Post>> GetAllPosts()
		{
			return await _context
				.Posts
				.Where(p => p.IsCompleted == false && p.IsDeleted == false)
				.ToListAsync();
		}

		public async Task<Post?> GetPostByIdAsync(int postId)
		{
			return await _context
				.Posts
				.FirstOrDefaultAsync(p => p.IsCompleted == false && p.IsDeleted == false && p.Id == postId)
		}

		public void UpdatePost(Post post)
		{
			_context.Update(post);
		}
	}
}
