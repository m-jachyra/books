using Azure.Storage.Blobs;
using Backend.Models;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Storage
{
    public class StorageService : IStorageService
    {
        private readonly BlobContainerClient _containerClient;
        
        public StorageService(BlobServiceClient blob)
        {
            _containerClient = blob.GetBlobContainerClient("pictures");
        }
        
        public async Task UploadFileAsync(IFormFile file, string fileName)
        {
            try
            {
                var blobClient = _containerClient.GetBlobClient(fileName);
                var result = await blobClient.UploadAsync(file.OpenReadStream(), true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        public async Task DeleteFileAsync(string fileName)
        {
            if (fileName.IsNullOrEmpty()) return;
            
            try
            {
                var blobClient = _containerClient.GetBlobClient(fileName);
                if (await blobClient.ExistsAsync())
                    await blobClient.DeleteAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}