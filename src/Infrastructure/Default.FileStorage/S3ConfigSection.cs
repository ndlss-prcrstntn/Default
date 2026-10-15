using Default.Abstractions;

namespace Default.FileStorage;

public class S3ConfigSection : IConfigSection
{
    public static readonly string Name = "s3";

    public string AccessKey { get; set; }

    public string AccessKeySecret { get; set; }

    public string BucketName { get; set; }

    public string Endpoint { get; set; }

    public string Region { get; set; }
}