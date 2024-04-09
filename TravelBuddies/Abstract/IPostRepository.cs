namespace TravelBuddies.Application.Abstract
{
	using TravelBuddies.Domain.Entities;

	public interface IPostRepository
	{
		Task<Post> CreatePostAsync(Post post);

		Task<Post> UpdateGroup(Post post, Group group);

		Task DeletePost(Post post);

		Task CompletePost(Post post);

		Task<Post> EditPostAsync(Post post);

		List<Post> GetAllPosts();

		Task<Post?> GetPostByIdAsync(int postId);
	}
}
