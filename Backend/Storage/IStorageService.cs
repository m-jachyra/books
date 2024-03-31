namespace Backend.Storage
{
    public interface IStorageService
    {
        public Task UploadFileAsync(IFormFile file, string fileName);
    }
}