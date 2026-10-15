namespace Default.FileStorage;

public sealed record FileObject(Stream Stream, string ContentType);

public interface IFileStorageService
{
    Task UploadAsync(string key, Stream stream, string contentType = "text/csv");
    Task<Stream> DownloadAsync(string key);
    Task<FileObject> DownloadObjectAsync(string key);
    Task DeleteAsync(string key, CancellationToken cancellationToken = default);
}
