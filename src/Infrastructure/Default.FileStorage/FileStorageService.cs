using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Default.FileStorage;

public class FileStorageService(IOptions<S3ConfigSection> options, IAmazonS3 s3, ILogger<FileStorageService> logger)
    : IFileStorageService
{
    private readonly string _bucketName = options.Value.BucketName;

    public async Task<Stream> DownloadAsync(string key)
    {
        var file = await DownloadObjectAsync(key);
        return file.Stream;
    }

    public async Task<FileObject> DownloadObjectAsync(string key)
    {
        try
        {
            logger.LogDebug("Downloading from S3. Bucket={Bucket}, Key={Key}", _bucketName, key);

            var response = await s3.GetObjectAsync(_bucketName, key);
            var contentType = MediaContentTypes.Resolve(key, response.Headers.ContentType);

            logger.LogDebug(
                "Download completed. Bucket={Bucket}, Key={Key}, ContentLength={Length}, ContentType={ContentType}",
                _bucketName, key, response.ContentLength, contentType);

            return new FileObject(response.ResponseStream, contentType);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Download failed. Bucket={Bucket}, Key={Key}", _bucketName, key);
            throw;
        }
    }

    public async Task UploadAsync(string key, Stream stream, string contentType = "text/csv")
    {
        try
        {
            if (stream.CanSeek)
                stream.Position = 0;

            var size = stream.CanSeek ? stream.Length : -1;
            logger.LogDebug("Uploading to S3. Bucket={Bucket}, Key={Key}, Size={Size} bytes",
                _bucketName, key, size);

            var request = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = key,
                InputStream = stream,
                ContentType = contentType,
                UseChunkEncoding = false,
                DisablePayloadSigning = false,
                DisableDefaultChecksumValidation = true
            };

            if (size >= 0)
                request.Headers.ContentLength = size;

            await s3.PutObjectAsync(request);

            logger.LogDebug("Upload completed. Bucket={Bucket}, Key={Key}", _bucketName, key);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Upload failed. Bucket={Bucket}, Key={Key}", _bucketName, key);
            throw;
        }
    }

    public async Task DeleteAsync(string key, CancellationToken cancellationToken = default)
    {
        try
        {
            logger.LogDebug("Deleting from S3. Bucket={Bucket}, Key={Key}", _bucketName, key);

            await s3.DeleteObjectAsync(_bucketName, key, cancellationToken);

            logger.LogDebug("Delete completed. Bucket={Bucket}, Key={Key}", _bucketName, key);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Delete failed. Bucket={Bucket}, Key={Key}", _bucketName, key);
            throw;
        }
    }
}
