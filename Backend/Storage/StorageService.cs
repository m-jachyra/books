using Azure.Storage.Blobs;

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
    }
}