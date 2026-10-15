using Default.Abstractions.Exceptions;

namespace Default.FileStorage;

/// <summary>
/// Shared image MIME / extension helpers for uploads and downloads.
/// </summary>
public static class MediaContentTypes
{
    public static readonly HashSet<string> AllowedImageContentTypes = new(StringComparer.OrdinalIgnoreCase)
    {
        "image/jpeg",
        "image/jpg",
        "image/png",
        "image/gif",
        "image/webp",
    };

    public static readonly HashSet<string> AllowedImageExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".jpg",
        ".jpeg",
        ".png",
        ".gif",
        ".webp",
    };

    /// <summary>
    /// Resolve Content-Type from object key / filename extension.
    /// Falls back to <paramref name="storedContentType"/> when extension is unknown.
    /// </summary>
    public static string Resolve(string? keyOrFileName, string? storedContentType = null)
    {
        var lower = (keyOrFileName ?? string.Empty).ToLowerInvariant();
        if (lower.EndsWith(".jpg") || lower.EndsWith(".jpeg")) return "image/jpeg";
        if (lower.EndsWith(".png")) return "image/png";
        if (lower.EndsWith(".gif")) return "image/gif";
        if (lower.EndsWith(".webp")) return "image/webp";
        if (lower.EndsWith(".svg")) return "image/svg+xml";

        if (!string.IsNullOrWhiteSpace(storedContentType)
            && !string.Equals(storedContentType, "application/octet-stream", StringComparison.OrdinalIgnoreCase)
            && !string.Equals(storedContentType, "binary/octet-stream", StringComparison.OrdinalIgnoreCase))
        {
            return storedContentType;
        }

        return "application/octet-stream";
    }

    /// <summary>
    /// Ensures the uploaded image is a browser-displayable format (rejects HEIC/AVIF/etc.).
    /// Returns normalized content type and file extension (including leading dot).
    /// </summary>
    public static (string ContentType, string Extension) EnsureAllowedImage(string? fileName, string? contentType)
    {
        var extension = Path.GetExtension(fileName ?? string.Empty).ToLowerInvariant();
        var normalizedType = (contentType ?? string.Empty).Split(';')[0].Trim().ToLowerInvariant();
        if (normalizedType == "image/jpg")
            normalizedType = "image/jpeg";

        var unsupported = extension is ".heic" or ".heif" or ".avif" or ".tiff" or ".tif"
                          || normalizedType is "image/heic" or "image/heif" or "image/avif" or "image/tiff";
        if (unsupported)
        {
            throw new ClientException(
                "Формат изображения не поддерживается. Загрузите JPEG, PNG, GIF или WebP.");
        }

        var extensionOk = !string.IsNullOrEmpty(extension) && AllowedImageExtensions.Contains(extension);
        var typeOk = !string.IsNullOrEmpty(normalizedType) && AllowedImageContentTypes.Contains(normalizedType);

        if (!extensionOk && !typeOk)
        {
            throw new ClientException(
                "Формат изображения не поддерживается. Загрузите JPEG, PNG, GIF или WebP.");
        }

        if (!extensionOk)
        {
            extension = normalizedType switch
            {
                "image/jpeg" => ".jpg",
                "image/png" => ".png",
                "image/gif" => ".gif",
                "image/webp" => ".webp",
                _ => ".jpg"
            };
        }

        if (!typeOk)
        {
            normalizedType = Resolve(extension);
        }

        return (normalizedType, extension);
    }
}
