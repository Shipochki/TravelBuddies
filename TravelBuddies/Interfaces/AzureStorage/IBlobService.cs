namespace TravelBuddies.Application.Interfaces.AzureStorage
{
	using Microsoft.AspNetCore.Http;

	public interface IBlobService
	{
		public Task<string> UploadImageAsync(IFormFile file);
	}
}
