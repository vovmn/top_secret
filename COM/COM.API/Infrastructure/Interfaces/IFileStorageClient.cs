namespace COM.API.Infrastructure.Interfaces
{
    public interface IFileStorageClient
    {
        Task<string> UploadFileAsync(Stream fileStream, string fileName, CancellationToken cancellationToken = default);
    }
}