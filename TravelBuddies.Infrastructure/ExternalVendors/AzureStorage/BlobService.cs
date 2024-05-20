namespace TravelBuddies.Infrastructure.ExternalVendors.AzureStorage
{
    using Azure.Storage.Blobs;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using TravelBuddies.Application.Common.Interfaces.AzureStorage;

    public class BlobService : IBlobService
    {
  //      private readonly BlobServiceClient _blobServiceClient;
  //      private readonly string? _containerName;

  //      public BlobService(IConfiguration configuration)
  //      {
  //          _blobServiceClient = new BlobServiceClient(configuration["AzureStorage:AzureBlobStorageConnectionString"]);
  //          _containerName = configuration["AzureStorage:ContainerClient"];
		//}

  //      public async Task<string> UploadImageAsync(IFormFile file)
  //      {
  //          if (file == null || file.Length == 0)
  //              return "Invalid file";

  //          BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
  //          BlobClient blobClient = containerClient.GetBlobClient(Guid.NewGuid().ToString());

  //          using (var stream = file.OpenReadStream())
  //          {
  //              await blobClient.UploadAsync(stream);
  //          }

  //          return blobClient.Uri.ToString();
  //      }
    }
}
