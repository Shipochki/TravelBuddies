namespace TravelBuddies.Application.Common.Interfaces.AzureStorage
{
    using Microsoft.AspNetCore.Http;

    public interface IBlobService
    {
        public Task<string> UploadImageAsync(IFormFile file);
    }
}
