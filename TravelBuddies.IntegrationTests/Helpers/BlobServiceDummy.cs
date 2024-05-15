namespace TravelBuddies.IntegrationTests.Helpers
{
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;
    using TravelBuddies.Application.Common.Interfaces.AzureStorage;

    public class BlobServiceDummy : IBlobService
	{
		public async Task<string> UploadImageAsync(IFormFile file)
		{
			string test = "testLink";
			return test;
		}
	}
}
