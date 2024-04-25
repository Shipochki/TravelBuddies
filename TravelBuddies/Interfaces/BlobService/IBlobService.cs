namespace TravelBuddies.Application.Interfaces.BlobService
{
	using Microsoft.AspNetCore.Http;
	using Microsoft.Extensions.Configuration;

	public interface IBlobService
	{
		public Task<string> UploadImageAsync(IFormFile file);
	}
}
