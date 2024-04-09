namespace TravelBuddies.Application.Abstract
{
	using TravelBuddies.Domain.Entities;

	public interface IPostRepository
	{
		Task CreatePostAsync(Post post);

		void UpdatePost(Post post);

		Task<List<Post>> GetAllPosts();

		Task<Post?> GetPostByIdAsync(int postId);
	}
}
